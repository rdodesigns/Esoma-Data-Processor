/**
 * @file
 * @author Ryan Orendorff <ryan@rdodesigns.com>
 *
 * @section DESCRIPTION
 *
 * Determine how closely a patient matches a real time training avatar.
 *
 * @section CONTACT
 *
 *  email: esoma@rdodesigns.com
 *    www: http://www.rdodesigns.com
 * github: https://github.com/rdodesigns
 *
 * @section LICENSE
 *
 * Copyright by Ryan Orendorff, 2012 under the New BSD License. See
 * LICENSE.txt for more details.
 */
using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm
{
  public class ExerciseAdherence : Algorithm
  {
    // All of the algorithm code is in this class so that
    // it can be easily moved.
    private MovementAlgo mov = new MovementAlgo();

    public ExerciseAdherence(){
      requiredDataFields = new string[] {"Skeleton"};
      System.Console.WriteLine("Initialised ExerciseAdherence calculator.");
    }

    // The data created is in total angular displacement from avatar in radians
    protected override void registerDataTypes(){
      data.Add("ExerciseAdherence", new float());
    }

    protected override void run(DataRecord.DataRecord incoming){
      // Sometimes the data trasmission is poor.
      Types.Skeleton skel = incoming.getData("Skeleton");
      if (skel._empty) return;

      // Must add the avatar's skeleton before the player's.
      mov.addAvatar(skel);
      mov.addPlayer(skel);

      // Calculate the total angular displacement.
      mov.run();
      data["ExerciseAdherence"] = mov.getFlail();
    }

  } // end class ExerciseAdherence

  internal class MovementAlgo
  {
    // Total number of records to keep, for averaging purposes.
    private int record_max = 30;
    // Last record_max total angular displacement values, for moving average.
    private Queue<double> hist_angular_disp = new Queue<double>();

    // Skeletons for the person and the avatar.
    private Types.Skeleton person;
    private Types.Skeleton avatar;

    // A double key limb dictionary of 3D vectors for the last 30 instances.
    // An example key is "Neck","Head".
    private Dictionary<Tuple<string, string>, LinkedList<double[]>>
      moved_joints =
        new Dictionary<Tuple<string, string>, LinkedList<double[]>>();
    // The list of keys for the dictionary.
    private List<Tuple<string, string>> joint_combos =
                                    new List<Tuple<string, string>>();
    // Should we be running? Don't calculate an angular displacement if the
    // count in moved_joints is less than 10.
    private bool _stop = true;

    // Incremented over time, calculations happen only after 10 avatars are
    // collected.
    private int stop_count = 0;

    public MovementAlgo(){
      // Set of rotational bodies to monitor.
      joint_combos.Add(Tuple.Create("Neck", "Head"));
      joint_combos.Add(Tuple.Create("Torso", "Neck"));
      joint_combos.Add(Tuple.Create("Left Shoulder", "Right Shoulder"));
      joint_combos.Add(Tuple.Create("Left Shoulder", "Left Elbow"));
      joint_combos.Add(Tuple.Create("Right Shoulder", "Right Elbow"));

      // Add the rotational body keys to the dictionary of avatar movement
      // vectors.
      foreach(Tuple<string, string> joint_combo in joint_combos)
        moved_joints.Add(joint_combo, new LinkedList<double[]>());
    }

    /**
     * Add avatar skeleton to the algorithm.
     *
     * @param skel Avatar's skeleton
     */
    public void addAvatar(Types.Skeleton skel){
      // Allow calculation of angular displacement only after 10 avatars are
      // collected.
      if (stop_count > 10){
        _stop = false;
      } else if (stop_count <= 10){
        stop_count++;
      }

      // This is a placeholder, it could be removed.
      avatar = skel;

      // Calculate the rotational body vector; the vector that points between the
      // two joints where the first joint is moved to the origin.
      foreach (Tuple<string, string> joint_combo in joint_combos){
        LinkedList<double[]> tmp = moved_joints[joint_combo];
        // Keep a constantly fresh set of record_max rotational body vectors.
        if (tmp.Count >= record_max)
          tmp.RemoveLast();
        tmp.AddFirst(moveJointCombo(skel, joint_combo));
      }
    }

    /// Add a player skeleton.
    public void addPlayer(Types.Skeleton skel){
      person = skel;
    }

    /**
     * Calculation of the total angular displacement for one point of time.
     *
     * This function tests the angular displacement of each joint against
     * the last record_max samples in order to account for a person matching
     * an avatar's movements but at a delay. It does this by looking for the
     * first local minimum of the angular displacement though a zero order
     * look ahead. If more precision is needed a higher order look ahead
     * should be performed.
     */
    public void run(){
      // TODO: Add a higher order look ahead.
      // If we have not collected 10 avatars, don't run.
      if (_stop) return;

      // Initial theta total over all joint pairs (rotational bodies).
      double theta_t = 0;

      // For each rotational body
      foreach(Tuple<string, string> joint_combo in joint_combos){
        double[] rel_person_vec = moveJointCombo(person, joint_combo);
        // The values that are equivalent to infinity due to their mathematical
        // impossibility (acos < pi/4)
        double theta_min = 2*Math.PI;
        double theta_last = 2*Math.PI;

        // For each of the last record_max vectors for the rotational body.
        foreach(double[] v in moved_joints[joint_combo]){
          double theta = calculateThetaOnPosition(v, rel_person_vec);

          // If we have a new min, set it to the min variable.
          if (theta <= theta_min)
            theta_min = theta;

          // If the min is equal to the last theta value, then the theta value
          // has only increased. Therefore, a minimum was reached.
          if (theta_min == theta_last) break;

          // The minimum was not reached, continue.
          theta_last = theta;
        } // end search for min theta for rotational body.

        theta_t += theta_min;
      } // end calculating thetas for each rotational body.

      // Function call to simplify average call.
      addToThetaRun(theta_t);
    }


    /**
     * Add the resulting total angular displacement to the running list.
     *
     * @param a The total angular displacement
     */
    private void addToThetaRun(double a){
      // We don't care about the direction of the list.
      if (hist_angular_disp.Count >= record_max) hist_angular_disp.Dequeue();
      hist_angular_disp.Enqueue(a);
    }

    /**
     * Gives the average of the running total angular displacement.
     *
     * @return The running angular displacement average over record_max points
     *         in time.
     */
    public double getFlail(){
      // TODO: Make this much more efficient through a running average.
      double sum = 0;

      foreach(double theta in hist_angular_disp)
        sum += theta;

      double avg = sum/hist_angular_disp.Count;
      if (double.IsNaN(avg)) return 0;
      return avg;
    }

    /**
     * Calculate the theta between two vectors sharing a common origin.
     *
     * The equbtion is @f[bcos\left(\frbc{b_x*b_x + b_y*b_y + b_z*b_z}{\sqrt{b_x^2 + b_y^2 + b_z^2}\sqrt{b_x^2 + b_y^2 + b_z^2}}\right)]
     *
     * @param a First 3D Vector
     * @param b Second 3D Vector, sharing same origin as 'a'
     * @return the angle between two vectors of common origin, as radians.
     */
    private double calculateThetaOnPosition(double[] a, double[] b){
      double theta =
        Math.Acos(
          (a[0]*b[0] + a[1]*b[1] + a[2]*b[2])
          /
          (Math.Pow(Math.Pow(a[0],2) + Math.Pow(a[1],2) + Math.Pow(a[2],2),0.5)
           *Math.Pow(Math.Pow(b[0],2) + Math.Pow(b[1],2) + Math.Pow(b[2],2),0.5)
          )
        );

      // Ocassionally this happens near 0 rads and asymtotic points.
      if (double.IsNaN(theta)) return 0;

      return theta;
    }

    /**
     * Calculate the angle of separation between two rotation matricies.
     *
     * This is done by the following function:
     * @f[acos\left(R_{11}R_{11}' + R_{21}R_{21}' + R_{31}R_{31}'\right)]
     *
     * @param a First rotation matrix.
     * @param b Second rotation matrix.
     * @return Angle between the two rotation matricies, as radians.
     */
    private double calculateThetaOnRotation(double[,] a, double[,] b){
      return Math.Acos(a[0,0]*b[0,0] + a[1,0]*b[1,0] + a[2,0]*b[2,0]);
    }

    /**
     * Make a relative 3D vector of a limb out of a skeleton and the two joints
     * in the limb.
     *
     * @param skel The skeleton to acquire the relative vector from.
     * @param joint_combo The joint combo to make the relative vector from,
     *                    where the first joint is the origin and the second
     *                    is where the relative vector points to.
     * @see moveRelative()
     * @return the 3D relative vector between the base joint and the
     *         extended joint.
     */
    private double[] moveJointCombo(Types.Skeleton skel,
                                    Tuple<string, string> joint_combo){
      double[] a = skel.getJointPositionByName(joint_combo.Item1);
      double[] b = skel.getJointPositionByName(joint_combo.Item2);

      return moveRelative(a, b);
    }

    /**
     * Find the vector that points from an origin point to an extended point.
     *
     * @param origin The translation vector.
     * @param point The vector to described in the translated system.
     * @return the 3D relative vector between the origin and point vector.
     */
    private double[] moveRelative(double[] origin, double[] point){
      return new double[] {point[0] - origin[0],
                           point[1] - origin[1],
                           point[2] - origin[2]};
    }

  } // end class MovementAlgo

} // end namespace Algorithm

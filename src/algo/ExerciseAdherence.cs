using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm
{
  public class ExerciseAdherence : Algorithm
  {
    private MovementAlgo mov = new MovementAlgo();

    public ExerciseAdherence(){
      requiredDataFields = new string[] {"Skeleton"};
      System.Console.WriteLine("Initialised ExerciseAdherence calculator.");
    }

    protected override void registerDataTypes(){
      data.Add("ExerciseAdherence", new float());
    }

    protected override void run(DataRecord.DataRecord incoming){
      Types.Skeleton skel = incoming.getData("Skeleton");
      if (skel._empty) return;
      mov.addAvatar(skel);
      mov.addPlayer(skel);
      mov.run();
      data["ExerciseAdherence"] = mov.getFlail();
    }

  } // end class ExerciseAdherence

  public class MovementAlgo
  {
    private Queue<double> bad_count = new Queue<double>();
    private Types.Skeleton person;
    private Types.Skeleton avatar;
    private Dictionary<Tuple<string, string>, LinkedList<double[]>> moved_joints = new Dictionary<Tuple<string, string>, LinkedList<double[]>>();
    private bool _stop = true;
    private List<Tuple<string, string>> joint_combos = new List<Tuple<string, string>>();
    private int record_max = 30;
    private int stop_count = 0;

    public MovementAlgo(){
      joint_combos.Add(Tuple.Create("Neck", "Head"));
      joint_combos.Add(Tuple.Create("Torso", "Neck"));
      joint_combos.Add(Tuple.Create("Left Shoulder", "Right Shoulder"));
      joint_combos.Add(Tuple.Create("Left Shoulder", "Left Elbow"));
      joint_combos.Add(Tuple.Create("Right Shoulder", "Right Elbow"));

      foreach(Tuple<string, string> joint_combo in joint_combos)
        moved_joints.Add(joint_combo, new LinkedList<double[]>());
    }

    public void addAvatar(Types.Skeleton skel){
      if (stop_count > 10){
        _stop = false;
      } else if (stop_count <= 10){
        stop_count++;
      }

      avatar = skel;
      foreach (Tuple<string, string> joint_combo in joint_combos){
        LinkedList<double[]> tmp = moved_joints[joint_combo];
        if (tmp.Count >= record_max)
          tmp.RemoveLast();
        tmp.AddFirst(moveJointCombo(skel, joint_combo));
      }
    }

    public void addPlayer(Types.Skeleton skel){
      person = skel;
    }

    public void run(){
      if (_stop) return;

      double theta_t = 0;

      foreach(Tuple<string, string> joint_combo in joint_combos){
        double[] rel_person_vec = moveJointCombo(person, joint_combo);
        double theta_min = 360;
        double theta_last = 360;

        //int j=0;
        foreach(double[] v in moved_joints[joint_combo]){
          double theta = calculateThetaOnPosition(v, rel_person_vec);
          if (theta <= theta_min)
            theta_min = theta;

          if (theta_min == theta_last) break;
          theta_last = theta;
          //System.Console.WriteLine(j++);
        }
        theta_t += theta_min;
      }

      addToThetaRun(theta_t);
    }


    private void addToThetaRun(double a){
      if (bad_count.Count >= record_max) bad_count.Dequeue();
      bad_count.Enqueue(a);
    }

    public double getFlail(){
      double sum = 0;

      foreach(double theta in bad_count)
        sum += theta;

      double avg = sum/bad_count.Count;
      if (double.IsNaN(avg)) return 0;
      return avg;
    }

    private double calculateThetaOnPosition(double[] a, double[] b){
      double theta = Math.Acos((a[0]*b[0] + a[1]*b[1] + a[2]*b[2])/(Math.Pow(Math.Pow(a[0],2) + Math.Pow(a[1],2) + Math.Pow(a[2],2),0.5)*Math.Pow(Math.Pow(b[0],2) + Math.Pow(b[1],2) + Math.Pow(b[2],2),0.5)));
      if (double.IsNaN(theta)) return 0;
      return theta;
    }

    private double calculateThetaOnRotation(double[,] a, double[,] b){
      return Math.Acos(a[0,0]*b[0,0] + a[1,1]*b[1,1] + a[2,2]*b[2,2]);
    }

    private double[] moveJointCombo(Types.Skeleton skel, Tuple<string, string> joint_combo){
      double[] a = skel.getJointPositionByName(joint_combo.Item1);
      double[] b = skel.getJointPositionByName(joint_combo.Item2);

      return moveRelative(a, b);
    }

    private double[] moveRelative(double[] origin, double[] point){
      return new double[] {point[0] - origin[0], point[1] - origin[1], point[2] - origin[2]};
    }
  }


} // end namespace Algorithm

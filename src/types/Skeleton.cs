using System;
using System.Collections.Generic;

namespace Types
{
  public class Skeleton
  {

    public bool _empty = false;
    protected Dictionary<string, double[]> joint_pos = new Dictionary<string, double[]>();
    protected Dictionary<string, double[,]> joint_rot = new Dictionary<string, double[,]>();
    //private string[] joint_names = new string[] { "Head", "Neck", "Torso", "Left Shoulder", "Left Elbow", "Left Hand", "Right Shoulder", "Right Elbow", "Right Hand", "Left Hip", "Left Knee", "Left Foot", "Right Hip", "Right Knee", "Right Foot" };
    private string[] joint_names = new string[] { "Head", "Neck", "Torso", "Left Shoulder", "Left Elbow",  "Right Shoulder", "Right Elbow"};

    public Skeleton(){_empty = true;}

    public Skeleton(double[][] pos, double[][,] rot){
      for(int i = 0; i < joint_names.Length; i++){
        joint_pos.Add(joint_names[i], pos[i]);
        joint_rot.Add(joint_names[i], rot[i]);
      }
    }

    public override string ToString(){
      if (_empty) return "\"\"";
      string output = "{";

      foreach (string joint in joint_names){
        output += "\"" + joint + "\":{\"Positions\":[";

        foreach (double pos in joint_pos[joint]){
          output += pos.ToString() + ",";
        }
        output = output.Remove(output.Length -1, 1) + "], \"Rotations\":[";

        for (int i = 0; i < joint_rot[joint].GetLength(0); i++){
          output += "[" + joint_rot[joint][i,0].ToString() + ",";
          output += joint_rot[joint][i,1].ToString() + ",";
          output += joint_rot[joint][i,2].ToString() + "],";
        }

        output = output.Remove(output.Length -1, 1) + "]},";
      }

      return output.Remove(output.Length -1, 1) + "}";
    }

    public double[] getJointPositionByName(string name){
      return joint_pos[name];
    }

    public double[,] getJointRotationByName(string name){
      return joint_rot[name];
    }

  }
}


using System;
using System.Threading;

namespace Device
{
  public class Zigfu : TCPDevice
  {
    public Zigfu(){name = "U";}
    //~ExampleDevice(){}

    protected override void registerDataTypes(){
      data.Add("Skeleton", new Types.Skeleton());
    }

    protected override void getInput(){
      try{
        data["Skeleton"] = new Types.Skeleton(parseInputforJointPositions(), parseInputforJointRotations());
      } catch (Exception ex) { System.Console.WriteLine("Ha, got ya!");}
    }

    private double[][] parseInputforJointPositions(){
      double[][] output = new double[15][];

      for (int i = 0; i < output.Length; i++){
        output[i] = new double[] {1,2,3};
      }

      return output;
    }

    private double[][] parseInputforJointRotations(){
      double[][] output = new double[15][];

      for (int i = 0; i < output.Length; i++){
        output[i] = new double[] {1,2,3};
      }

      return output;
    }

  } // end class PulseOx2
} // end namespace Device

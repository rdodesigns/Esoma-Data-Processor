using System;
using System.Threading;
using System.Linq;

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
      double[][] super_array;
      try{
      super_array = _new_data.Split('|').Select(o => o.Split(';').Select(p => Convert.ToDouble(p)).ToArray()).ToArray();
      } catch (Exception ex){System.Console.WriteLine("Bad String from Zigfu"); return;}
      data["Skeleton"] = new Types.Skeleton(parseInputforJointPositions(super_array), parseInputforJointRotations(super_array));
    }

    private double[][] parseInputforJointPositions(double[][] arr){
      double[][] output = new double[7][];

      for (int i = 0; i < arr.GetLength(0); i++)
        output[i] = arr[i].Skip(9).Take(3).ToArray();

      return output;
    }

    private double[][,] parseInputforJointRotations(double[][] arr){
      double[][,] output = new double[7][,];

      for (int i = 0; i < output.GetLength(0); i++) {
        double [,] tmp = new double[3,3];
        for(int j = 0; j < 3; j++)
          for(int k = 0; k < 3; k++)
            tmp[j,k] = arr[i][j+k];

        output[i] = tmp;
      }
      return output;
    }

  } // end class PulseOx2
} // end namespace Device

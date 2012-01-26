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
      string input = "0.9983112;-0.01626002;-0.05577506;0.02002447;0.9975101;0.06760682;0.05453657;-0.0686096;0.9961507;102.3909;549.7584;2006.674|0.9983112;-0.01626002;-0.05577506;0.02002447;0.9975101;0.06760682;0.05453657;-0.0686096;0.9961507;98.36789;349.3521;1993.091|0.9984211;-0.009708869;-0.05533157;0.01343999;0.9976306;0.06745805;0.05454519;-0.06809528;0.9961855;95.71545;152.4653;1979.778|0.121994;0.9921995;0.02564164;-0.9911118;0.120398;0.05658238;0.05305381;-0.03231645;0.9980686;-44.54462;350.7418;2001.011|0.08050051;0.9729415;0.2165745;-0.9963314;0.07221375;0.0459215;0.02903928;-0.2194767;0.9751854;-76.16106;93.59958;1994.366|0.1177306;-0.9930432;-0.002169813;0.9802972;0.1165679;-0.1594654;0.158609;0.01664689;0.9872011;241.2804;347.9624;1985.171|-0.001448914;-0.9736617;-0.2279928;0.9831008;0.04034948;-0.1785634;0.1830597;-0.2243987;0.9571491;272.5956;83.82285;1984.594";
      double[][] pos = parseInputforJointPositions(input);
      double[][,] rot = parseInputforJointRotations(input);
      foreach (double[] val in pos)
        foreach (double val2 in val)
          Console.WriteLine(val2);
      foreach (double[,] val in rot)
        foreach (double val2 in val)
        Console.WriteLine(val2);
    }

    protected override void getInput(){
      //System.Console.Write(_new_data + "\n\n");

      string input = "0.9983112;-0.01626002;-0.05577506;0.02002447;0.9975101;0.06760682;0.05453657;-0.0686096;0.9961507;102.3909;549.7584;2006.674|0.9983112;-0.01626002;-0.05577506;0.02002447;0.9975101;0.06760682;0.05453657;-0.0686096;0.9961507;98.36789;349.3521;1993.091|0.9984211;-0.009708869;-0.05533157;0.01343999;0.9976306;0.06745805;0.05454519;-0.06809528;0.9961855;95.71545;152.4653;1979.778|0.121994;0.9921995;0.02564164;-0.9911118;0.120398;0.05658238;0.05305381;-0.03231645;0.9980686;-44.54462;350.7418;2001.011|0.08050051;0.9729415;0.2165745;-0.9963314;0.07221375;0.0459215;0.02903928;-0.2194767;0.9751854;-76.16106;93.59958;1994.366|0.1177306;-0.9930432;-0.002169813;0.9802972;0.1165679;-0.1594654;0.158609;0.01664689;0.9872011;241.2804;347.9624;1985.171|-0.001448914;-0.9736617;-0.2279928;0.9831008;0.04034948;-0.1785634;0.1830597;-0.2243987;0.9571491;272.5956;83.82285;1984.594";
      data["Skeleton"] = new Types.Skeleton(parseInputforJointPositions(input), parseInputforJointRotations(input));
    }

    private double[][] parseInputforJointPositions(string input){
      try{
      double[][] output = new double[7][];
      string[] joints = input.Split('|');

      for (int i = 0; i < output.GetLength(0); i++) {
        double[] arr = joints[i].Split(';').Select(o => Convert.ToDouble(o)).ToArray();
        output[i] = arr.Skip(9).Take(3).ToArray();
      }

      return output;
      } catch (Exception ex) {System.Console.WriteLine("C"); throw ex;}

    }

    private double[][,] parseInputforJointRotations(string input){
      try{
      double[][,] output = new double[7][,];
      string[] joints = input.Split('|');
      //output[i] = new double[,] {{ (double[]) arr.Take(3)}, {(double[]) arr.Skip(3).Take(3)}, {(double[]) arr.Skip(6).Take(3)}};

      for (int i = 0; i < output.GetLength(0); i++) {
        double [,] tmp = new double[3,3];
        double[] arr = (double[]) joints[i].Split(';').Select(o => Convert.ToDouble(o)).ToArray().Take(9).ToArray();
        for(int j = 0; j < 3; j++)
          for(int k = 0; k < 3; k++)
            tmp[j,k] = arr[j+k];

        output[i] = tmp;
      }
      return output;
      } catch (Exception ex) {System.Console.WriteLine("R"); throw ex;}
    }

  } // end class PulseOx2
} // end namespace Device

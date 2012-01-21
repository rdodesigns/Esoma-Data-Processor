using System;
using pulsoximeter;

namespace Device
{
  public class PulseOx : Device
  {
    OnyxII po;

    public PulseOx(bool mode)
    {
      po = new OnyxII(mode);
      System.Console.WriteLine("Created PulseOx object, simulated {0}.", mode);
      for (int i = 0; i < 10; i++) {
        System.Console.WriteLine("{0}", po.GetHrAndSpo2());
      }
    }

    ~PulseOx(){
      System.Console.WriteLine("PulseOx object deallocated.");
    }

    public override void start() {

    }

    public override void getInput(){}
    public override void parseData(){}

  }
}

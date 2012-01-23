using System;
using System.Threading;
using pulsoximeter;

namespace Device
{
  public class ExampleDevice : Device
  {
    public ExampleDevice(){name = "ExampleDevice";}
    //~ExampleDevice(){}

    protected override void registerDataTypes(){
      data.Add("Example Int", new int());
      data.Add("Example Float", new float());
    }

    protected override void getInput(){
      data["Example Int"] = (int) new Random().Next(0,100);
      data["Example Float"] = (float) new Random().Next(0,100);
    }

  } // end class PulseOx2
} // end namespace Device

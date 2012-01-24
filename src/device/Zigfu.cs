using System;
using System.Threading;

namespace Device
{
  public class Zigfu : TCPDevice
  {
    public Zigfu(){name = "U";}
    //~ExampleDevice(){}

    protected override void registerDataTypes(){
      data.Add("Zigfu", "");
    }

    protected override void getInput(){
      this.data["Zigfu"] = _new_data;
    }

  } // end class PulseOx2
} // end namespace Device

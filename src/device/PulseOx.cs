using System;
using System.Threading;
using pulsoximeter;

namespace Device
{
  public class PulseOx : Device
  {
    private bool _mode;
    OnyxII po;

    public PulseOx(bool mode){
      _mode = mode;
      name = "PulseOx";
      po = new OnyxII(_mode);
    }
    //~PulseOx(){}

    protected override void registerDataTypes(){
      data.Add("Heart Rate", new int());
      data.Add("Blood Oxygenation", new int());
    }

    protected override void getInput(){
      string str = po.GetHrAndSpo2();
      int[] vals = parseData(str);
      data["Heart Rate"] = vals[0];
      data["Blood Oxygenation"] = vals[1];
      if (_mode)
        System.Threading.Thread.Sleep(1000);
    }

    private int[] parseData(string str){
      string[] vals = str.Split(',');
      return new int[] {int.Parse(vals[0]), int.Parse(vals[1])};
    }

  } // end class PulseOx
} // end namespace Device

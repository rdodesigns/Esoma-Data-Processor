using System;
using System.Threading;
using pulsoximeter;

namespace Device
{
  public class PulseOx : Device
  {
    private bool running = false;
    private bool mode;
    OnyxII po;

    public PulseOx(DataRecord.DataRecordGenerator drg): base(drg) {}
    //~PulseOx(){}

    protected override void init() {
      this.name = "PulseOx";
      this.mode = true;

      if (!running){
        po = new OnyxII(mode);
        running = true;
      }
    }

    protected override void registerDataTypes(){
      data.Add("Heart Rate", new int());
      data.Add("Blood Oxygenation", new int());
    }

    protected override void getInput(){
      string str = po.GetHrAndSpo2();
      int[] vals = parseData(str);
      data["Heart Rate"] = vals[0];
      data["Blood Oxygenation"] = vals[1];
    }

    private int[] parseData(string str){

      string[] vals = str.Split(',');
      return new int[] {int.Parse(vals[0]), int.Parse(vals[1])};
    }

    public void setSimulateMode(){
      this.mode = true;
    }

  } // end class PulseOx
} // end namespace Device

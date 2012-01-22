using System;
using System.Threading;
using pulsoximeter;

namespace Device
{
  public class PulseOx : Device
  {
    private bool _running = false;
    private bool _mode;
    OnyxII po;

    public PulseOx(DataRecord.DataRecordGenerator drg): base(drg) {}
    //~PulseOx(){}

    protected override void init() {
      this.name = "PulseOx";
      this._mode = true;

      if (!_running){
        po = new OnyxII(_mode);
        _running = true;
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
      this._mode = true;
    }

  } // end class PulseOx
} // end namespace Device

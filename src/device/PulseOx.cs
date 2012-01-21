using System;
using pulsoximeter;
using System.Threading;

namespace Device
{
  public class PulseOx : Device
  {
    private bool running = false;
    private bool mode;
    OnyxII po;

    public PulseOx(AutoResetEvent autoEvent, DataRecord.DataRecordGenerator drg, bool mode): base(autoEvent, drg)
    {
      data.Add("Heart Rate", new int());
      data.Add("Blood Oxygenation", new int());

      this.mode = mode;
      System.Console.WriteLine("Created PulseOx object, simulated {0}.", mode);
      this.registerDataForRecord();
    }

    ~PulseOx(){
      System.Console.WriteLine("PulseOx object deallocated.");
    }

    public override void start() {
      if (!running){
        po = new OnyxII(mode);

        for (int i = 0; i < 10; i++) {
          this.acquireData();
        }

        running = true;
      }
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

  }
}

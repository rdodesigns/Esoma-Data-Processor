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
      this.mode = mode;
      registerDataForRecord(drg);
      System.Console.WriteLine("Created PulseOx object, simulated {0}.", mode);

    }

    ~PulseOx(){
      System.Console.WriteLine("PulseOx object deallocated.");
    }

    public override void start() {
      if (!running){
        po = new OnyxII(mode);

        for (int i = 0; i < 10; i++) {
          getInput();
        }

        running = true;
      }
    }

    public override void getInput(){
      string str = po.GetHrAndSpo2();
      int[] vals = parseData(str);
      System.Console.WriteLine("HR:{0}\tBO:{1}",vals[0], vals[1]);
    }

    private int[] parseData(string str){
      string[] vals = str.Split(',');
      return new int[] {int.Parse(vals[0]), int.Parse(vals[1])};
    }

    protected override void registerDataForRecord(DataRecord.DataRecordGenerator drg){
      drg.addDataField("Heart Rate", new int());
      drg.addDataField("Blood Oxygenation", new int());
    }

  }
}

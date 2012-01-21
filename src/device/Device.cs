using System;
using System.Threading;

namespace Device
{
  public abstract class Device
  {
    private AutoResetEvent autoEvent;
    private DataRecord.DataRecordGenerator drg;

    public Device(AutoResetEvent autoEvent, DataRecord.DataRecordGenerator drg)
    {
      System.Console.WriteLine("Created Device object.");
      this.autoEvent = autoEvent;
      this.drg = drg;
      System.Console.WriteLine("autoEvent set.");
    }

    public abstract void start();
    public abstract void getInput();
    protected abstract void registerDataForRecord(DataRecord.DataRecordGenerator dr);

    private void sendToDataRecord() {}

  }
}

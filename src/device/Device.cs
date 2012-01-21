using System;
using System.Threading;

namespace Device
{
  public abstract class Device
  {
    private AutoResetEvent autoEvent;
    private DataRecord.DataRecordGenerator drg;
    protected object[,] dataTypes;

    public Device(AutoResetEvent autoEvent, DataRecord.DataRecordGenerator drg)
    {
      System.Console.WriteLine("Created Device object.");
      this.autoEvent = autoEvent;
      this.drg = drg;
    }

    public abstract void start();
    public abstract void getInput();

    protected void registerDataForRecord(){
      for(int i = 0; i < dataTypes.GetLength(0); i++)
        drg.addDataField((string) dataTypes[i,0], dataTypes[i,1]);
    }

    private void sendToDataRecord() {}

  }
}

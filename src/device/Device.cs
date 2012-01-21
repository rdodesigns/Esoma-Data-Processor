using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Device
{
  public abstract class Device
  {
    private AutoResetEvent autoEvent;
    private DataRecord.DataRecordGenerator drg;
    protected SortedList data = new SortedList();

    public Device(AutoResetEvent autoEvent, DataRecord.DataRecordGenerator drg)
    {
      System.Console.WriteLine("Created Device object.");
      this.autoEvent = autoEvent;
      this.drg = drg;
    }

    public abstract void start();
    public abstract void getInput();

    protected void registerDataForRecord(){
      for(int i = 0; i < data.Count; i++)
        drg.addDataField((string) data.GetKey(i), data.GetByIndex(i));
    }

    protected void sendToDataRecord() {
      drg.addValues(data);
    }

  }
}

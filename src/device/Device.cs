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
    protected string name;
    protected SortedList data = new SortedList();

    public Device(AutoResetEvent autoEvent, DataRecord.DataRecordGenerator drg)
    {
      this.init();
      System.Console.WriteLine("Created {0} Device object.", this.name);
      this.autoEvent = autoEvent;
      this.drg = drg;
      this.registerDataTypes();
      this.registerWithDataRecord();
    }

    protected abstract void init();
    protected abstract void registerDataTypes();

    public abstract void start();
    protected abstract void getInput();

    public void acquireData(){
      this.getInput();
      this.sendToDataRecord();
    }

    protected void registerWithDataRecord(){
      for(int i = 0; i < data.Count; i++)
        drg.addDataField((string) data.GetKey(i), data.GetByIndex(i));
    }

    protected void sendToDataRecord() {
      drg.addValues(data);
    }

  }
}

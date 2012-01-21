using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Device
{
  public abstract class Device
  {
    private DataRecord.DataRecordGenerator drg;
    protected string name;
    protected SortedList data = new SortedList();
    protected bool stopped = false;

    public Device(DataRecord.DataRecordGenerator drg)
    {
      this.init();
      System.Console.WriteLine("Created {0} Device object.", this.name);
      this.drg = drg;
      this.registerDataTypes();
      this.registerWithDataRecord();
    }

    protected abstract void init();
    protected abstract void registerDataTypes();

    public void start(){
      if(!stopped){
        Thread t = new Thread(acquireData);
        t.Start();
      } else
        System.Console.WriteLine("Device {0} is stopped due to an error.", this.name);
    }

    protected abstract void getInput();

    public void acquireData(){
      while (true){
        System.Threading.Thread.Sleep(1000);
        this.getInput();
        this.sendToDataRecord();
      }
    }

    private void registerWithDataRecord(){
      for(int i = 0; i < data.Count; i++){
        if (!drg.addDataField((string) data.GetKey(i), data.GetByIndex(i))){
          this.stopped = true;
          unregisterWithDataRecord();
        }
      }
    }

    private void unregisterWithDataRecord(){
      for(int i = 0; i < data.Count; i++)
          drg.removeKey((string) data.GetKey(i));
    }

    private void sendToDataRecord() {
      lock(drg.loc){
        drg.addValues(data);
      }
    }

  }
}

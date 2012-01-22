using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Device
{
  public abstract class Device
  {
    protected string name;
    protected SortedList data = new SortedList();
    private DataRecord.DataRecordGenerator _drg;
    private bool _stopped = false;
    private volatile bool _end = false;

    public Device(DataRecord.DataRecordGenerator drg)
    {
      this.init();
      System.Console.WriteLine("Created {0} Device object.", this.name);
      this._drg = drg;
      this.registerDataTypes();
      this.registerWithDataRecord();
    }

    protected abstract void init();
    protected abstract void registerDataTypes();

    public void start(){
      if(!_stopped){
        Thread t = new Thread(acquireData);
        t.Start();
      } else
        System.Console.WriteLine("ERROR: Device {0} is stopped due to an error.", this.name);
    }

    public void stop() { _end = true;}

    protected abstract void getInput();

    public void acquireData(){
      while (!_end){
        this.getInput();
        this.sendToDataRecord();
      }
    }

    private void registerWithDataRecord(){
      try {
        for(int i = 0; i < data.Count; i++)
          _drg.addDataField((string) data.GetKey(i), data.GetByIndex(i));
      }
      catch (Exception ex){
        System.Console.WriteLine(ex);
        this._stopped = true;
        unregisterWithDataRecord();
      }
    }

    private void unregisterWithDataRecord(){
      for(int i = 0; i < data.Count; i++)
          _drg.removeKey((string) data.GetKey(i));
    }

    private void sendToDataRecord() {
      lock(_drg.loc){
        try {
          _drg.addValues(data);
        }
        catch (Exception ex) {
          System.Console.WriteLine("ERROR: {0} Could not send to DataRecordGenerator", name);
          throw ex;
        }
      }
    }

  } // end class Device
} // end namespace Device

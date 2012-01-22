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

    protected Device(DataRecord.DataRecordGenerator drg)
    {
      this.init();
      System.Console.WriteLine("Created {0} Device object.", this.name);
      this._drg = drg;
      this.registerDataTypes();
      _drg.registerWithDataRecord(data);
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
        _drg.sendToDataRecord(data);
      }
    }

  } // end class Device
} // end namespace Device

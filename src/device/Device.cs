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
    private volatile bool _end = false; // Will be thread accessed.

    // Methods that require override
    protected abstract void init();
    protected abstract void registerDataTypes();
    protected abstract void getInput();

    protected Device()
    {
      this.init();
      System.Console.WriteLine("Created {0} Device object.", this.name);
      this.registerDataTypes();
      data.Add("Timestamp", new DateTime());
    }

    // Start data acquisition thread
    public void start(){
      if(!_stopped){
        Thread t = new Thread(acquireData);
        t.Start();
      } else
        System.Console.WriteLine("ERROR: Device {0} is stopped due to an error.", this.name);
    }

    // Stop the thread man!
    public void stop() { _end = true;}

    // Thread runs this code.
    public void acquireData(){
      while (!_end){
        this.getInput();
        data["Timestamp"] = getTimestamp();
        _drg.sendToDataRecord(data);
      }
    }

    public SortedList getData(){ return data; }

    private DateTime getTimestamp()
    {
        return DateTime.UtcNow;
    }

    public void setDataRecordGenerator(DataRecord.DataRecordGenerator drg){
      this._drg = drg;
    }

  } // end class Device
} // end namespace Device

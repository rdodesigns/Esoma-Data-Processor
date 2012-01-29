using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Device
{
  public abstract class Device
  {
    public event EventHandler<DataRecord.DataEvent> RaiseDataEvent;

    protected string name;
    protected Hashtable data = new Hashtable();
    private bool _stopped = false;
    protected volatile bool _end = false; // Will be thread accessed.

    // Methods that require override
    protected abstract void registerDataTypes();
    protected abstract void getInput();

    protected Device()
    {
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
    public virtual void acquireData(){
      while (!_end){
        try{
          data["Timestamp"] = getTimestamp();
          this.getInput();
        } catch(Exception ex) {System.Console.WriteLine("Could not get data."); throw ex;}
        OnRaiseDataEvent(new DataRecord.DataEvent(data));
      }
    }

    public Hashtable getData(){ return data; }

    protected DateTime getTimestamp()
    {
        return DateTime.UtcNow;
    }

    protected virtual void OnRaiseDataEvent(DataRecord.DataEvent e)
    {
        EventHandler<DataRecord.DataEvent> handler = RaiseDataEvent;

        if (handler != null)
        {
            handler(this, e);
        }
    }


  } // end class Device


} // end namespace Device

using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Device
{
  public abstract class Device
  {
    public event EventHandler<DeviceDataEvent> RaiseDeviceDataEvent;

    protected string name;
    protected SortedList data = new SortedList();
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
        OnRaiseDeviceDataEvent(new DeviceDataEvent(data));
      }
    }

    public SortedList getData(){ return data; }

    private DateTime getTimestamp()
    {
        return DateTime.UtcNow;
    }

    protected virtual void OnRaiseDeviceDataEvent(DeviceDataEvent e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately after the null check and before the event is raised.
        EventHandler<DeviceDataEvent> handler = RaiseDeviceDataEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Use the () operator to raise the event.
            handler(this, e);
        }
    }


  } // end class Device

  public class DeviceDataEvent : EventArgs
  {
      public DeviceDataEvent(SortedList incoming)
      {
          device_data = incoming;
      }
      private SortedList device_data;
      public SortedList data
      {
          get { return device_data; }
      }
  } // end class DeviceDataEvent

} // end namespace Device

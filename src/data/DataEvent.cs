using System;
using System.Collections;

namespace DataRecord
{
  public class DataEvent : EventArgs
  {
      public DataEvent(Hashtable incoming)
      {
          device_data = incoming;
      }
      private Hashtable device_data;
      public Hashtable data
      {
          get { return device_data; }
      }
  } // end class DataEvent
}

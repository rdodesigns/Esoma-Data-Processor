using System;
using System.Collections;

namespace DataRecord
{
  public class DataEvent : EventArgs
  {
      public DataEvent(SortedList incoming)
      {
          device_data = incoming;
      }
      private SortedList device_data;
      public SortedList data
      {
          get { return device_data; }
      }
  } // end class DataEvent
}

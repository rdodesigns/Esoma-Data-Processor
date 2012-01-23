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

  public class DataRecordEvent : EventArgs
  {
      public DataRecordEvent(DataRecord incoming)
      {
          _data = incoming;
      }
      private DataRecord _data;
      public DataRecord data_record
      {
          get { return _data; }
      }

  } // end class DataEvent
}

using System;

namespace Client
{
  public abstract class Client
  {

    protected Client() { }

    protected virtual void catchRecord(object sender, DataRecord.DataRecordEvent dre){
      System.Console.WriteLine("Received by Client.");
      sendRecord(dre.data_record);
    }

    public void attachToPool(DataRecord.DataRecordPool drp){
      drp.RaiseDataRecord += catchRecord;
    }

    protected abstract void sendRecord(DataRecord.DataRecord dr);
  } // end class Client
} // end namespace Client


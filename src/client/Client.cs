using System;

namespace Client
{
  public abstract class Client
  {
    protected Client() { }

    protected virtual void catchRecord(object sender, DataRecord.DataRecordEvent dre){
      System.Console.WriteLine("Received by Client.");
      dre.data_record.printRecord();
    }

    public void attachToPool(DataRecord.DataRecordPool drp){
      drp.RaiseDataRecord += catchRecord;
    }
  } // end class Client
} // end namespace Client


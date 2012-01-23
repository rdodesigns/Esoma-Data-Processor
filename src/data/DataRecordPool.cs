using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DataRecord
{
  public class DataRecordPool
  {
    private volatile bool _running = false;
    private List<Algorithm.Algorithm> _algos;
    private Queue _data_record_queue = Queue.Synchronized(new Queue());
    public DataRecordPool(DataRecordGenerator drp)
    {
      _algos = drp.algos;
      drp.RaiseDataRecord += acquireDataRecord;

      _running = true;
      Thread consumer = new Thread(() =>
          {
              while (_running)
              {
                  if (_data_record_queue.Count > 0)
                  {
                      DataRecord dr = (DataRecord) _data_record_queue.Dequeue();
                      dr.printRecord();
                  }
              }
          });

        consumer.IsBackground = true;
        consumer.Start();
    }

    private void acquireDataRecord(object sender,DataRecordEvent rec){
      System.Console.WriteLine("DataRecordPool gained a DataRecord.");
      _data_record_queue.Enqueue(rec.data_record);
    }



  } // end class Algorithm
} // end namespace Algorithm

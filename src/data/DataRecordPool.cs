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
    private Queue<DataRecord> _data_record_queue = new Queue<DataRecord>();
    private readonly object _locker = new object();

    public DataRecordPool(DataRecordGenerator drp)
    {
      _algos = drp.algos;
      drp.RaiseDataRecord += acquireDataRecord;

      _running = true;
      Thread t = new Thread(consume);
      t.Start();
    }

    //~DataRecordPool() { }

    public void destroyPool(){
      _running = false;
    }

    private void acquireDataRecord(object sender,DataRecordEvent rec){
      System.Console.WriteLine("DataRecordPool gained a DataRecord.");
      lock(_locker) {
        _data_record_queue.Enqueue(rec.data_record);
        Monitor.Pulse(_locker);
      }
    }

    private void consume(){
      while(_running){
        DataRecord dr;
        lock(_locker){
          while (_data_record_queue.Count == 0) Monitor.Wait(_locker);
          dr = _data_record_queue.Dequeue();
        }
        if (dr == null) return;
        dr.printRecord();
      }
    }



  } // end class Algorithm
} // end namespace Algorithm

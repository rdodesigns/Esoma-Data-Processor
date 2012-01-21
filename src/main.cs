using System;
using System.Threading;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecordGenerator drg = new DataRecord.DataRecordGenerator();
    Device.PulseOx po = new Device.PulseOx(new AutoResetEvent(false), drg, true);
    po.start();
    drg.listKeys();
  }
}



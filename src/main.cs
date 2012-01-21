using System;
using System.Threading;
using System.Collections;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecordGenerator drg = new DataRecord.DataRecordGenerator();
    Device.PulseOx po = new Device.PulseOx(new AutoResetEvent(false), drg, true);
    po.start();

    IList keys = drg.getKeys();

    System.Console.WriteLine("Keys in Data Records.");
    for(int i = 0; i < keys.Count; i++)
      System.Console.WriteLine(keys[i]);
  }
}



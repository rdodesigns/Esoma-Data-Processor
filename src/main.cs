using System;
using System.Threading;
using System.Collections;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecordGenerator drg = new DataRecord.DataRecordGenerator();
    Device.PulseOx po = new Device.PulseOx(drg);
    po.start();

    while(Console.ReadKey(true).Key != ConsoleKey.Escape){ }

    po.stop();
    //System.Environment.Exit(0);
  }

} // end class DataProcessor



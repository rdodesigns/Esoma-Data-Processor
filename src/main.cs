using System;
using System.Threading;
using System.Collections;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecordGenerator drg = new DataRecord.DataRecordGenerator();
    Device.PulseOx po = new Device.PulseOx(drg);
    Device.PulseOx2 po2 = new Device.PulseOx2(drg);
    po.start();
    po2.start();

    while(Console.ReadKey(true).Key != ConsoleKey.Escape){ }

    po.stop();
    po2.stop();
    //System.Environment.Exit(0);
  }

} // end class DataProcessor



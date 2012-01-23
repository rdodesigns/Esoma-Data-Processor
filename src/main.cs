using System;
using System.Threading;
using System.Collections;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecordGenerator drg = new DataRecord.DataRecordGenerator();

    drg.registerDevice(new Device.PulseOx(true));
    drg.registerAlgorithm(new Algorithm.Met());

    drg.startGenerating();

    while(Console.ReadKey(true).Key != ConsoleKey.Escape){ }

    drg.stopGenerating();
    //System.Environment.Exit(0);
  }

} // end class DataProcessor



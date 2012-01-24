using System;
using System.Threading;
using System.Collections;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecordGenerator drg = new DataRecord.DataRecordGenerator();
    DataRecord.DataRecordPool drp = new DataRecord.DataRecordPool(drg);

    drg.registerDevice(new Device.PulseOx(0));
    Device.Zigfu zf = new Device.Zigfu();
    drg.registerDevice(zf);
    drg.registerAlgorithm(new Algorithm.Met());

    Client.Zigfu client = new Client.Zigfu();
    client.attachToPool(drp);
    client.attachTCPServer(zf.serv);

    drg.startGenerating();

    while(Console.ReadKey(true).Key != ConsoleKey.Escape){ }

    drg.stopGenerating();
    drp.destroyPool();
    //System.Environment.Exit(0);
  }

} // end class DataProcessor



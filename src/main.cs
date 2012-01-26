using System;
using System.Threading;
using System.Collections;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecordGenerator drg = new DataRecord.DataRecordGenerator();
    DataRecord.DataRecordPool drp = new DataRecord.DataRecordPool(drg);

    drg.registerPatient(new Patient.Patient());
    drg.registerDevice(new Device.PulseOx(0));
    Device.Zigfu zf = new Device.Zigfu();
    drg.registerDevice(zf);
    drg.registerAlgorithm(new Algorithm.Met());

    Client.Zigfu client = new Client.Zigfu(zf.serv);
    Client.Indivo ind = new Client.Indivo(zf.serv);

    client.attachToPool(drp);
    ind.attachToPool(drp);

    drg.startGenerating();

    while(Console.ReadKey(true).Key != ConsoleKey.Escape){ }

    drg.stopGenerating();
    drp.destroyPool();
    System.Environment.Exit(0); // Needed to kill TCPServer
  }

} // end class DataProcessor



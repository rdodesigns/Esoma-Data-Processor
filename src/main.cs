using System;
using System.Threading;
using System.Collections;
using EsomaTCP.TCPServer;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecordGenerator drg = new DataRecord.DataRecordGenerator();
    DataRecord.DataRecordPool drp = new DataRecord.DataRecordPool(drg);

    TCPServer serv = new TCPServer();

    drg.registerPatient(new Patient.Patient());

    drg.registerDevice(new Device.PulseOx(0));
    drg.registerDevice(new Device.Zigfu(serv));
    drg.registerDevice(new Device.Indivo(serv));

    drg.registerAlgorithm(new Algorithm.Met());
    drg.registerAlgorithm(new Algorithm.ExerciseAdherence());

    Client.Zigfu client = new Client.Zigfu(serv);
    Client.Indivo ind = new Client.Indivo(serv);

    client.attachToPool(drp);
    ind.attachToPool(drp);

    drg.startGenerating();

    while(Console.ReadKey(true).Key != ConsoleKey.Escape){ }

    drg.stopGenerating();
    drp.destroyPool();
    System.Environment.Exit(0); // Needed to kill TCPServer
  }

} // end class DataProcessor



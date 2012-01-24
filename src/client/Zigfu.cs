using System;
using EsomaTCP.TCPServer;

namespace Client
{
  public class Zigfu : TCPClient
  {
    public Zigfu(TCPServer serv) : base(serv) {
      client = "UNITY";
    }


    //protected virtual void catchRecord(object sender, DataRecord.DataRecordEvent dre){
      //dre.data_record.printRecord();
    //}
  } // end class Client
} // end namespace Client



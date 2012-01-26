using System;
using EsomaTCP.TCPServer;

namespace Client
{
  public class Indivo : TCPClient
  {
    public Indivo(TCPServer serv) : base(serv) {
      client = "INDIVO";
    }


    //protected virtual void catchRecord(object sender, DataRecord.DataRecordEvent dre){
      //dre.data_record.printRecord();
    //}
  } // end class Client
} // end namespace Client



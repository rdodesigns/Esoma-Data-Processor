using System;
using EsomaTCP.TCPServer;

namespace Client
{
  public class Zigfu : Client
  {
    private TCPServer serv;
    public Zigfu() { }

    public void attachTCPServer(TCPServer serv){this.serv = serv;}

    protected override void sendRecord(DataRecord.DataRecord dr){
      serv.SendToClient(dr.getRecordAsJson(), "UNITY");
    }

    //protected virtual void catchRecord(object sender, DataRecord.DataRecordEvent dre){
      //dre.data_record.printRecord();
    //}
  } // end class Client
} // end namespace Client



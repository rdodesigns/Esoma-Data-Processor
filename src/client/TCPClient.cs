using System;
using EsomaTCP.TCPServer;

namespace Client
{
  public abstract class TCPClient : Client
  {
    protected TCPServer serv;
    protected string client;
    protected TCPClient(TCPServer serv) {
      if (serv != null)
        this.serv = serv;
    }

    public void attachTCPServer(TCPServer serv){this.serv = serv;}

    protected override void sendRecord(DataRecord.DataRecord dr){
      System.Console.WriteLine("Sending.");
      if (serv == null) throw new System.ArgumentNullException("TCPServer", "Client requires a reference to a TCP server.");

      //System.Console.WriteLine("Sending data to {0}.", client);
      try {
        serv.SendToClient(dr.getRecordAsJson(), client);
      } catch (Exception ex) {
        System.Console.WriteLine("Could not send data to {0}.", client);
      }
    }

  } // end class Client
} // end namespace Client


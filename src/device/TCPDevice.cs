using System;
using System.Threading;
using EsomaTCP.TCPServer;

namespace Device
{
  public abstract class TCPDevice : Device
  {
    public TCPServer serv;

    protected string _new_data;
    private AutoResetEvent auto = new AutoResetEvent (false);

    protected TCPDevice(){
      serv = new TCPServer();
      serv.DataManager += new DataManager(onDataReceived);
      serv.StartServer();
    }

    public override void acquireData(){
      while (!_end && auto.WaitOne()){
        this.getInput();
        data["Timestamp"] = getTimestamp();
        OnRaiseDataEvent(new DataRecord.DataEvent(data));
      }
    }

    protected void onDataReceived(string sendername, string data){
      if (!(sendername == name)) return;

      _new_data = data.Remove(0,2);
      Console.WriteLine(data);
      auto.Set();
    }

  } // end class TCPDevice
} // end namespace Device

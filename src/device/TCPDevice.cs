using System;
using System.Threading;
using EsomaTCP.TCPServer;

namespace Device
{
  public abstract class TCPDevice : Device
  {
    private TCPServer _serv;

    protected string _new_data;
    private AutoResetEvent auto = new AutoResetEvent (false);

    protected TCPDevice(TCPServer serv){
      serv.DataManager += new DataManager(onDataReceived);
      _serv = serv;
    }

    public override void acquireData(){
      while (!_end && auto.WaitOne()){
        try{
          this.getInput();
          data["Timestamp"] = getTimestamp();
        } catch (Exception ex) {System.Console.WriteLine("Could not get data."); throw ex;}
        OnRaiseDataEvent(new DataRecord.DataEvent(data));
      }
    }

    protected void onDataReceived(string sendername, string data){
      if (!(sendername == name)) return;

      _new_data = data.Remove(0,2);
      //Console.WriteLine(data);
      auto.Set();
    }

  } // end class TCPDevice
} // end namespace Device

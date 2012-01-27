using System;
using System.Threading;
using System.Collections;
using EsomaTCP.TCPServer;
using EsomaSharedDocuments;

namespace Patient
{
  public class Patient
  {
    public Hashtable data = new Hashtable();
    private TCPServer _serv;
    private string _patient = "";
    private AutoResetEvent _patient_found = new AutoResetEvent (false);
    private string name = "I";

    public Patient(TCPServer serv){
      _serv = serv;
      _serv.DataManager += new DataManager(onDataReceived);

      data.Add("Mass", 60.0);
      data.Add("Height", 160.0);// cm

      EsomaSharedDocuments.IndivoInitialization _indivoInit;

      bool indivo_init=false;

      do{
        if(serv.IsClientConnected("INDIVO")){
          _indivoInit = new IndivoInitialization("rpoole","rpoole-dope75");
          Console.WriteLine("INITIALIZE INDIVO:" +_indivoInit.ToString());
          serv.SendToClient("INITIALIZE|"+_indivoInit.ToString()+"|","INDIVO");
          indivo_init=true;
        } else System.Threading.Thread.Sleep(1000);
      } while((indivo_init == false));

    }

    public void waitForPatient(){
      _patient_found.WaitOne();

      double weight = 60.0;
      double height = 160.0;
      System.Console.WriteLine(_patient.Remove(0,4));
      IndivoExercisePlan plan = new IndivoExercisePlan(_patient.Remove(0,4));
      if (plan == null) System.Console.WriteLine("{0} BOOO.", plan.Age);
      data.Add("Age", plan.Age);
      data.Add("Gender", plan.Gender);
      data.Add("AccountID", plan.AccountID);
      data.Add("Plan", plan.Plan);
      data.Add("RMR", calculateRMR(height, weight, plan.Age, plan.Gender));
    }

    private void onDataReceived(string sendername, string data){
      if (!(sendername == name)) return;

      _patient_found.Set();
      _patient = data;
    }

    private double calculateRMR(double height, double weight, int years, string gender){
      if (gender == "Male")
        return 66.5 + 5*height + 13.7*weight - 6.8*years;
      else
        return 655.1 + 1.8*height + 9.6*weight - 4.7*years;
    }
  }
}

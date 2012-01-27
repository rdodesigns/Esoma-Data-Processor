using System;
using System.Collections;
using EsomaTCP.TCPServer;

namespace Patient
{
  public class Patient
  {
    public Hashtable data = new Hashtable();
    private TCPServer _serv;

    public Patient(TCPServer serv){
      _serv = serv;
      _serv.DataManager += new DataManager(onDataReceived);

      double weight = 60.0;
      string gender = "Female";
      int age = 60;
      double height = 160.0;

      data.Add("Mass", 60.0);
      data.Add("Gender", "Female");
      data.Add("Age", 60);
      data.Add("Height", 160.0);// cm
      data.Add("RMR", calculateRMR(height, weight, age, gender));
    }

    onDataReceived(string sendername, string data){
      if (!(sendername == name)) return;


    }

    private double calculateRMR(double height, double weight, int years, string gender){
      if (gender == "Male")
        return 66.5 + 5*height + 13.7*weight - 6.8*years;
      else
        return 655.1 + 1.8*height + 9.6*weight - 4.7*years;
    }
  }
}

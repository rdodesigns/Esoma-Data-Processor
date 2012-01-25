using System;
using System.Dynamic;

namespace Algorithm
{
  public class Met : Algorithm
  {
    private float VO2;

    public Met(){
      requiredDataFields = new string[] {"Heart Rate", "Blood Oxygenation", "Mass"};
      System.Console.WriteLine("Initialised Met calculator.");
    }

    protected override void registerDataTypes(){
      data.Add("MET", new float());
      data.Add("kCal/min", new float());
    }

    protected override void run(DataRecord.DataRecord incoming){
      double VO2 = 14;
      data["MET"] = VO2 / 3.5; // in mL kg-1 min-1
      data["kCal/min"] = VO2*incoming.getData("Mass")*5.05;
    }

  } // end cladd Met
} // end namespace Algorithm

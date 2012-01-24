using System;
using System.Dynamic;

namespace Algorithm
{
  public class ExampleAlgorithm : Algorithm
  {
    public ExampleAlgorithm(){
      requiredDataFields = new string[] {"Blood Oxygenation"};
      System.Console.WriteLine("Initialised ExampleClass calculator.");
    }

    protected override void registerDataTypes(){
      data.Add("ExampleAlgo", new float());
    }

    protected override void run(DataRecord.DataRecord incoming){
      data["ExampleAlgo"] = incoming.getData("Blood Oxygenation") + 12.2;
    }

  } // end cladd Met
} // end namespace Algorithm

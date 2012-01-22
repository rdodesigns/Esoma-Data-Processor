using System;

namespace Algorithm
{
  public class ExampleAlgorithm : Algorithm
  {
    private float algorithm_variable;

    public ExampleAlgorithm(){
      System.Console.WriteLine("Initialised ExampleAlgorithm calculator.");
    }

    protected override void registerDataTypes(){
      data.Add("Example Algo", new float());
    }

    protected override void run(){
      data["Example Algo"] = 4.4;
    }

  } // end cladd Met
} // end namespace Algorithm

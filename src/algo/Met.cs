using System;

namespace Algorithm
{
  public class Met : Algorithm
  {
    private float met;

    public Met(DataRecord.DataRecordGenerator drg):base(drg){ }

    protected override void init(){
      System.Console.WriteLine("Initialised Met calculator.");
    }

    protected override void registerDataTypes(){
      data.Add("MET", new float());
    }

    protected override void run(){
      data["MET"] = 4.4;
    }

  } // end cladd Met
} // end namespace Algorithm

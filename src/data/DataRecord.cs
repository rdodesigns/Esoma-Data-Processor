using System;

namespace DataRecord
{
  public class DataRecord
  {
    private float[,] joints;
    private int hr;
    private int bo;
    private float met;
    //private float kcal;
    private string exercise_id;
    private string command;

    public DataRecord()
    {
      System.Console.WriteLine("Created DataRecord object.");

      joints = new float[15,3];
    }
  }
}

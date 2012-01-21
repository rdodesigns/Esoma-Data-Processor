using System;
using System.Collections;

namespace DataRecord
{
  public class DataRecord
  {
    SortedList data;
    public DataRecord(SortedList data)
    {
      this.data = new SortedList(data);
      //System.Console.WriteLine("Created DataRecord object.");
      //for(int i = 0; i < data.Count; i++)
        //System.Console.WriteLine("{0}: {1}", data.GetKey(i), data.GetByIndex(i));
    }
  }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace DataRecord
{
  public class DataRecordGenerator
  {
    private SortedList data = new SortedList();

    public DataRecordGenerator()
    {
      System.Console.WriteLine("Created DataRecordGenerator object.");
    }

    public void addDataField(string key, object val){
      if (key == null){
        System.Console.WriteLine("Could not add {0} to DataRecordGenerator.", val);
        return;
      }
      data.Add(key, val);
    }

    public void removeKey(string key){
      data.Remove(key);
    }

    public void setOrderOfKeys(string[] order){
      return;
    }

    public void listKeys(){
      System.Console.Write("Keys in DataRecordGenerator");
      for(int i = 0; i < data.Count; i++)
        System.Console.Write(", {0} ({1})", data.GetKey(i), data.GetByIndex(i));
      System.Console.Write("\n");
    }

    public void addValues(SortedList incoming){
      for(int i=0; i < incoming.Count; i++){
        this.data[incoming.GetKey(i)] = incoming.GetByIndex(i);
        System.Console.WriteLine("{0} {1}", incoming.GetKey(i), incoming.GetByIndex(i));
      }
    }

  }
}

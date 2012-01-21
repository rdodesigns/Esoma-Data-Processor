using System;
using System.Collections;
using System.Collections.Generic;

namespace DataRecord
{
  public class DataRecordGenerator
  {
    private Hashtable data = new Hashtable();
    private List<string> order = new List<string>();

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
      order.Add(key);
    }

    public bool removeKey(string key){
      return order.Remove(key);
    }

    public void setOrderOfKeys(string[] order){
      return;
    }

    public void listKeys(){
      System.Console.Write("Keys in DataRecordGenerator");
      foreach(string key in order){
        System.Console.Write(", {0}", key);
      }
      System.Console.Write("\n");
    }

  }
}

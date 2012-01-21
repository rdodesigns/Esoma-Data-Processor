using System;
using System.Collections;
using System.Collections.Generic;

namespace DataRecord
{
  public class DataRecordGenerator
  {
    private SortedList data = new SortedList();
    public System.Object loc = new System.Object();

    public DataRecordGenerator()
    {
      System.Console.WriteLine("Created DataRecordGenerator object.");
    }

    public bool addDataField(string key, object val){
      if (key == null || key == ""){
        System.Console.WriteLine("ERROR: Could not add {0} to DataRecordGenerator.", val);
        return false;
      }

      if (data.ContainsKey(key)){
        System.Console.WriteLine("ERROR: Key already exists.");
        return false;
      }

      data.Add(key, val);
      return true;
    }

    public void removeKey(string key){
      data.Remove(key);
    }

    public void setOrderOfKeys(string[] order){
      return;
    }

    public IList getKeys(){
      return data.GetKeyList();
    }

    public void addValues(SortedList incoming){
      for(int i=0; i < incoming.Count; i++)
        this.data[incoming.GetKey(i)] = incoming.GetByIndex(i);
      new DataRecord(data);
    }

  }
}

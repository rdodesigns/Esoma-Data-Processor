using System;
using System.Collections;
using System.Collections.Generic;

namespace DataRecord
{
  public class DataRecordGenerator
  {
    private SortedList _data = new SortedList();
    public System.Object loc = new System.Object();

    public DataRecordGenerator()
    {
      System.Console.WriteLine("Created DataRecordGenerator object.");
    }

    public void addDataField(string key, object val){
      if (key == null || key == "")
        throw new System.MemberAccessException("Could not add " + val + " to DataRecordGenerator, key is null");

      if (_data.ContainsKey(key)){
        throw new System.MemberAccessException("Key already exists.");
      }

      _data.Add(key, val);
    }

    public void removeKey(string key){
      _data.Remove(key);
    }

    public void setOrderOfKeys(string[] order){
      return;
    }

    public IList getKeys(){
      return _data.GetKeyList();
    }

    public void addValues(SortedList incoming){
      try{
        for(int i=0; i < incoming.Count; i++)
          this._data[incoming.GetKey(i)] = incoming.GetByIndex(i);
        new DataRecord(_data);
      } catch (Exception ex){ throw ex; }
    }

    public void registerWithDataRecord(SortedList data){
      try {
        for(int i = 0; i < data.Count; i++)
          addDataField((string) data.GetKey(i), data.GetByIndex(i));
      }
      catch (Exception ex){
        System.Console.WriteLine(ex);
        unregisterWithDataRecord(data);
      }
    }

    private void unregisterWithDataRecord(SortedList data){
      for(int i = 0; i < data.Count; i++)
          removeKey((string) data.GetKey(i));
    }

    public void sendToDataRecord(SortedList data) {
      lock(loc){
        try {
          addValues(data);
        }
        catch (Exception ex) {
          System.Console.WriteLine("ERROR: Could not send to DataRecordGenerator");
          throw ex;
        }
      }
    }

  } // end class DataRecordGenerator
} // end namespace DataRecord

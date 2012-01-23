using System;
using System.Collections;
using System.Collections.Generic;

namespace DataRecord
{
  public class DataRecordGenerator
  {
    public System.Object loc = new System.Object();
    private SortedList _data = new SortedList();
    private List<Algorithm.Algorithm> algos = new List<Algorithm.Algorithm>();
    private List<Device.Device> devices = new List<Device.Device>();

    public DataRecordGenerator()
    {
      System.Console.WriteLine("Created DataRecordGenerator object.");
    }

    public void removeKey(string key){ _data.Remove(key); }

    public IList getKeys(){ return _data.GetKeyList(); }

    public bool ContainsKey(string key){return _data.ContainsKey(key);}

    public void addDataField(string key, object val){
      if (key == null || key == "")
        throw new System.MemberAccessException("Could not add " +
          val + " to DataRecordGenerator, key is null");

      if (_data.ContainsKey(key)){
        throw new System.MemberAccessException("Key already exists.");
      }

      _data.Add(key, val);
    }

    public void addValues(SortedList incoming){
      try{
        for(int i=0; i < incoming.Count; i++)
          _data[incoming.GetKey(i)] = incoming.GetByIndex(i);
        new DataRecord(_data);
      } catch (Exception ex){ throw ex; }
    }

    public void registerDataFieldWithDataRecord(SortedList data){
      try {
        for(int i = 0; i < data.Count; i++)
          addDataField((string) data.GetKey(i), data.GetByIndex(i));
      }
      catch (Exception ex){
        System.Console.WriteLine(ex);
        unregisterDataFieldWithDataRecord(data);
      }
    }

    private void unregisterDataFieldWithDataRecord(SortedList data){
      for(int i = 0; i < data.Count; i++)
          removeKey((string) data.GetKey(i));
    }

    public void registerAlgorithm(Algorithm.Algorithm algo){
      registerDataFieldWithDataRecord(algo.getDataFields());
      algos.Add(algo);
    }

    public void registerDevice(Device.Device dev){
      registerDataFieldWithDataRecord(dev.getData());
      devices.Add(dev);
      dev.RaiseDeviceDataEvent += sendToDataRecord;
    }

    public void startGenerating(){
      foreach(Device.Device dev in devices){
        dev.start();
      }
    }

    public void stopGenerating(){
      foreach(Device.Device dev in devices){
        dev.stop();
      }
    }

    public void sendToDataRecord(object sender, Device.DeviceDataEvent data) {
      lock(loc){
        try {
          addValues(data.data);
        }
        catch (Exception ex) {
          System.Console.WriteLine("ERROR: Could not send to " +
            "DataRecordGenerator");
          throw ex;
        }
      }
    }



  } // end class DataRecordGenerator
} // end namespace DataRecord

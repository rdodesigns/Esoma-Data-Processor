using System;
using System.Collections;
using System.Collections.Generic;

namespace DataRecord
{
  public class DataRecordGenerator
  {
    private System.Object loc = new System.Object();
    public event EventHandler<DataRecordEvent> RaiseDataRecord;

    private Hashtable _data = new Hashtable();
    public List<Algorithm.Algorithm> algos = new List<Algorithm.Algorithm>();
    private List<Device.Device> devices = new List<Device.Device>();

    public DataRecordGenerator()
    {
      System.Console.WriteLine("Created DataRecordGenerator object.");
      _data.Add("Timestamp", new DateTime());
    }

    public bool ContainsKey(string key){return _data.ContainsKey(key);}

    public void addDataField(string key, object val){
      if (key == null || key == "")
        throw new System.MemberAccessException("Could not add " +
          val + " to DataRecordGenerator, key is null");

      if (key == "Timestamp") return;

      if (_data.ContainsKey(key)){
        throw new System.MemberAccessException("Key " + key + " already exists.");
      }


      _data.Add(key, val);
    }

    public void addValues(Hashtable incoming){
      try{
        foreach (DictionaryEntry e in incoming)
          _data[e.Key] = e.Value;
        OnRaiseDataRecordEvent(new DataRecordEvent(new DataRecord(_data)));
      } catch (Exception ex){ throw ex; }
    }

    public void registerDataFieldWithDataRecord(Hashtable data){
      try {
        foreach(DictionaryEntry e in data)
          addDataField((string) e.Key, e.Value);
      }
      catch (Exception ex){
        System.Console.WriteLine(ex);
        unregisterDataFieldWithDataRecord(data);
      }
    }

    private void unregisterDataFieldWithDataRecord(Hashtable data){
      foreach(DictionaryEntry e in data)
          _data.Remove((string) e.Key);
    }

    public void registerAlgorithm(Algorithm.Algorithm algo){
      if(!checkForDataFields(algo.requiredDataFields))
        throw new System.ArgumentException("Algorithm requires key that is non-existant.");
      algos.Add(algo);
    }

    public void registerDevice(Device.Device dev){
      registerDataFieldWithDataRecord(dev.getData());
      devices.Add(dev);
      dev.RaiseDataEvent += sendToDataRecord;
    }

    public void registerPatient(Patient.Patient p){
      registerDataFieldWithDataRecord(p.data);
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

    public void sendToDataRecord(object sender, DataEvent data) {
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

    private bool checkForDataFields(string[] fields){
      foreach(string field in fields)
        if(!_data.ContainsKey(field)) return false;
      return true;
    }

    protected virtual void OnRaiseDataRecordEvent(DataRecordEvent e)
    {
        EventHandler<DataRecordEvent> handler = RaiseDataRecord;
        if (handler != null)
        {
            handler(this, e);
        }
    }

  } // end class DataRecordGenerator
} // end namespace DataRecord

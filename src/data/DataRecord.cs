using System;
using System.Collections;
using System.Collections.Generic;

namespace DataRecord
{
  public class DataRecord
  {
    private readonly object _locker = new object();
    private Hashtable data;
    public List<string> updated_fields = new List<string>();

    public DataRecord(Hashtable data)
    {
      this.data = new Hashtable(data);
      //System.Console.WriteLine("Created DataRecord object.");
    }

    public void printRecord(){
      foreach(DictionaryEntry e in data)
        System.Console.WriteLine("{0}: {1}", e.Key, e.Value);
      System.Console.WriteLine("");
    }

    public void addData(Hashtable incoming){
      lock(_locker){
        try{
          foreach (DictionaryEntry e in incoming)
            data[e.Key] = e.Value;
        } catch (Exception ex){ throw ex; }
      }
    }

    public dynamic getData(string key){
      return data[key];
    }

    public string getRecordAsJson(){
      string output = "{";
      foreach(DictionaryEntry e in data){
        output += "\"" + e.Key + "\":";
        Type type = e.Value.GetType();
        if (type == typeof(string)){
          output += "\"" + (string) e.Value + "\"";
        } else if (type == typeof(DateTime)){
          output += "\"" + ((DateTime)e.Value).ToString("yyyy-MM-dd HH:mm:ss.ffff") + "\"";
        } else if (type == typeof(int[])){
          output += "[";
          foreach(int var in (int[]) e.Value)
            output  += var.ToString() + ",";
          output = output.Remove(output.Length - 1, 1) + "]";
        } else if (type == typeof(double[])){
          output += "[";
          foreach(double var in (double[]) e.Value)
            output  += var.ToString() + ",";
          output = output.Remove(output.Length - 1, 1) + "]";
        //} else if (type == typeof(Types.Skeleton)){
            //output += (Type.Skeleton) e.Value.ToString();
        } else{
          output += e.Value.ToString();
        }
        output += ",";
      }

      return output.Remove(output.Length -1, 1) + "}";
    }


//str.Remove(str.Length - 1, 1);
  } // end class DataRecord
} // end namespace DataRecord

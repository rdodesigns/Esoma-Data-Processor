using System;
using System.Collections;

namespace DataRecord
{
  public class DataRecord
  {
    private readonly object _locker = new object();
    private Hashtable data;

    public DataRecord(Hashtable data)
    {
      this.data = new Hashtable(data);
      System.Console.WriteLine("Created DataRecord object.");
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

    public Hashtable getData(){
      return data;
    }

  } // end class DataRecord
} // end namespace DataRecord

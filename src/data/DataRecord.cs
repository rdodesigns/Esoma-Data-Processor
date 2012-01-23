using System;
using System.Collections;

namespace DataRecord
{
  public class DataRecord
  {
    private Hashtable _data;
    public DataRecord(Hashtable data)
    {
      this._data = new Hashtable(data);
      System.Console.WriteLine("Created DataRecord object.");
    }

    public void printRecord(){
      foreach(DictionaryEntry e in _data)
        System.Console.WriteLine("{0}: {1}", e.Key, e.Value);
      System.Console.WriteLine("");
    }
  } // end class DataRecord
} // end namespace DataRecord

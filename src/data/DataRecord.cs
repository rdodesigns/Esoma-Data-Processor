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
    }
  }
}

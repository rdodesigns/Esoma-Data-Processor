using System;
using System.Collections;

namespace Algorithm
{
  public abstract class Algorithm
  {
    protected SortedList data = new SortedList();

    protected Algorithm(DataRecord.DataRecordGenerator drg)
    {
      this.init();
      this.registerDataTypes();
      drg.registerDataFieldWithDataRecord(data);
      drg.registerAlgorithm(this);
    }

    protected abstract void init();
    protected abstract void registerDataTypes();
    protected abstract void run();

  } // end class Algorithm
} // end namespace Algorithm

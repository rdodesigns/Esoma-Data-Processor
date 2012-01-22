using System;
using System.Collections;

namespace Algorithm
{
  public abstract class Algorithm
  {
    protected SortedList data = new SortedList();
    private DataRecord.DataRecordGenerator _drg;

    protected Algorithm(DataRecord.DataRecordGenerator drg)
    {
      this.init();
      this._drg = drg;
      this.registerDataTypes();
      _drg.registerWithDataRecord(data);
    }

    protected abstract void init();
    protected abstract void registerDataTypes();
    protected abstract void run();

  } // end class Algorithm
} // end namespace Algorithm

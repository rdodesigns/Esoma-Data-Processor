using System;
using System.Collections;

namespace Algorithm
{
  public abstract class Algorithm
  {
    protected SortedList data = new SortedList();

    protected abstract void registerDataTypes();
    protected abstract void run();

    protected Algorithm()
    {
      this.registerDataTypes();
    }

    public SortedList getDataFields() {return data;}


  } // end class Algorithm
} // end namespace Algorithm

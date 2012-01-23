using System;
using System.Collections;

namespace Algorithm
{
  public abstract class Algorithm
  {
    protected Hashtable data = new Hashtable();
    public string[] requiredDataFields;

    protected abstract void registerDataTypes();
    protected abstract void run();

    protected Algorithm()
    {
      this.registerDataTypes();
    }

    public Hashtable getData() {return data;}


  } // end class Algorithm
} // end namespace Algorithm

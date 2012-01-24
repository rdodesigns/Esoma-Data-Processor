using System;
using System.Collections;

namespace Algorithm
{
  public abstract class Algorithm
  {
    protected Hashtable data = new Hashtable();
    public string[] requiredDataFields;

    protected abstract void registerDataTypes();
    protected abstract void run(DataRecord.DataRecord incoming);

    public void process(ref DataRecord.DataRecord incoming){
      try{
        this.run(incoming);
      } catch (Exception ex){ throw ex;}
      incoming.addData(data);
    }

    protected Algorithm()
    {
      this.registerDataTypes();
    }

    public Hashtable getData() {return data;}

  } // end class Algorithm
} // end namespace Algorithm

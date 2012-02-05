/**
 * @file
 * @author Ryan Orendorff <ryan@rdodesigns.com>
 *
 * @section DESCRIPTION
 *
 * Abstract class for running algorithms on DataRecords.
 *
 * @section CONTACT
 *
 *  email: esoma@rdodesigns.com
 *    www: http://www.rdodesigns.com
 * github: https://github.com/rdodesigns
 *
 * @section LICENSE
 *
 * Copyright by Ryan Orendorff, 2012 under the New BSD License. See
 * LICENSE.txt for more details.
 */

using System;
using System.Collections;

namespace Algorithm
{
  public abstract class Algorithm
  {
    // Data to be added to DataRecord
    protected Hashtable data = new Hashtable();
    // Fields required for the calculation, these are those collected from the
    // Device classes.
    public string[] requiredDataFields;

    // Register the data types to be added to the DataRecord after the
    // algorithm runs. Use only "data.Add(string key, object initial_value)"
    // to add data to the DataRecord object.
    protected abstract void registerDataTypes();

    // Code to run against the incoming data record.
    protected abstract void run(DataRecord.DataRecord incoming);

    // Process the DataRecord. This
    public void process(ref DataRecord.DataRecord incoming){
      bool runme = true;
      try{
        foreach (string field in requiredDataFields){
          if (!incoming.updated_fields.Contains(field))
              runme = false;
        }
        if (runme) this.run(incoming);
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

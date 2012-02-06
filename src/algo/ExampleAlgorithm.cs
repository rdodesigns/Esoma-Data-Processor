/**
 * @file
 * @author Ryan Orendorff <ryan@rdodesigns.com>
 *
 * @section DESCRIPTION
 *
 * Example of how to use the Algorithm class.
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
using System.Dynamic;

namespace Algorithm
{
  public class ExampleAlgorithm : Algorithm
  {
    public ExampleAlgorithm(){
      // Specify which DataRecord fields are needed to perform the calculation.
      requiredDataFields = new string[] {"Blood Oxygenation"};
      System.Console.WriteLine("Initialised ExampleClass calculator.");
    }

    /**
     * Tell the DataRecord what kind of data to expect.
     *
     * The inside of this fuction should consists of statements like the
     * following.
     *   data.Add("String Key", new DataType);
     */
    protected override void registerDataTypes(){
      data.Add("ExampleAlgo", new float());
    }

    /**
     * Code to run on the newly acquried data. Each algorithm is
     * run asychronously.
     *
     * This function should include pieces like the following
     * to add values to the DataRecord.
     *   data["String Key"] = value;
     */
    protected override void run(DataRecord.DataRecord incoming){
      data["ExampleAlgo"] = incoming.getData("Blood Oxygenation") + 12.2;
    }

  } // end cladd Met
} // end namespace Algorithm

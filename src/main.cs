using System;
using pulsoximeter;

class DataProcessor
{
  public static void Main()
  {
    DataRecord.DataRecord dr = new DataRecord.DataRecord();
    Device.PulseOx po = new Device.PulseOx(true);
  }
}



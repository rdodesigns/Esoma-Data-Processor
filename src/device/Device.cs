using System;

namespace Device
{
  public abstract class Device
  {
    public Device()
    {
      System.Console.WriteLine("Created Device object.");
    }

    public abstract void start();
    public abstract void getInput();
    public abstract void parseData();


    private void sendToDataRecord() {}

  }
}

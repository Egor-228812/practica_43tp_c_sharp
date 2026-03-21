
Device[] devices =
{
    new Smartphone(),
    new Laptop()
};

foreach (Device device in devices)
{
    device.TurnOn();
    device.TurnOff();
    Console.WriteLine();
}
public abstract class Device
{
    public abstract void TurnOn();

    public virtual void TurnOff()
    {
        Console.WriteLine("Device is turning off");
    }
}

public class Smartphone : Device
{
    public override void TurnOn()
    {
        Console.WriteLine("Smartphone is turning on");
    }

    public override void TurnOff()
    {
        Console.WriteLine("Smartphone is turning off");
    }
}

public class Laptop : Device
{
    public override void TurnOn()
    {
        Console.WriteLine("Laptop is turning on");
    }

    public override void TurnOff()
    {
        Console.WriteLine("Laptop is turning off");
    }
}

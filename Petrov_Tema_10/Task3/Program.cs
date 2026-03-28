using Task3;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        SmartHomeHub hub = new SmartHomeHub();

        Light light = new Light();
        Thermostat thermostat = new Thermostat();
        Alarm alarm = new Alarm();

        hub.AddDevice(light);
        hub.AddDevice(thermostat);
        hub.AddDevice(alarm);

        Console.WriteLine("Установка режима 'День':");
        hub.SetState("День");
        Console.WriteLine();

        Console.WriteLine("Установка режима 'Ночь':");
        hub.SetState("Ночь");
        Console.WriteLine();

        Console.WriteLine("Установка режима 'Вечеринка':");
        hub.SetState("Вечеринка");
    }
}
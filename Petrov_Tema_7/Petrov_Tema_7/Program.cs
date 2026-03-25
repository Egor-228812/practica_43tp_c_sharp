using Task1;
class Programm
{
    static void Main()
    {
        Car cr = new Car();
        Console.WriteLine("Введите скорость ТС: ");
        int spd = int.Parse(Console.ReadLine());
        try
        {
            cr.SetSpeed(spd);
        }
        catch (SpeedLimitExceededException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch(Exception ex) 
        {
            Console.WriteLine("Выявлена другая ошибка");
        }
        Console.WriteLine("Завершено");

    }
}
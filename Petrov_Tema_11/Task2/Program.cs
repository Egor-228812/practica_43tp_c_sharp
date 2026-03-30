using Task2;
class Program
{
    static void Main()
    {
        try
        {
            ICar car = new BasicCar();
            Console.WriteLine("Базовая комплектация:");
            Console.WriteLine($"Оснащение: {car.GetFeatures()}");
            Console.WriteLine($"Цена: {car.GetPrice():F2} руб.");

            car = new SunroofDecorator(car);
            Console.WriteLine("\nС люком:");
            Console.WriteLine($"Оснащение: {car.GetFeatures()}");
            Console.WriteLine($"Цена: {car.GetPrice():F2} руб.");

            car = new NavigationDecorator(car);
            Console.WriteLine("\n+ навигация:");
            Console.WriteLine($"Оснащение: {car.GetFeatures()}");
            Console.WriteLine($"Цена: {car.GetPrice():F2} руб.");

            car = new LeatherSeatsDecorator(car);
            Console.WriteLine("\n+ кожаные сиденья:");
            Console.WriteLine($"Оснащение: {car.GetFeatures()}");
            Console.WriteLine($"Цена: {car.GetPrice():F2} руб.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
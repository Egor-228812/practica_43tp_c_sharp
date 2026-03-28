using Task1;
class Program
{
    static void Main()
    {
        UISettings settings1 = UISettings.GetInstance();
        UISettings settings2 = UISettings.GetInstance();

        Console.WriteLine($"Текущая тема: {settings1.GetTheme()}");
        settings1.SetTheme("тёмная");
        Console.WriteLine($"После изменения через settings1: {settings1.GetTheme()}");
        Console.WriteLine($"Через settings2: {settings2.GetTheme()}");

        if (ReferenceEquals(settings1, settings2))
            Console.WriteLine("Оба объекта являются одним экземпляром.");
        else
            Console.WriteLine("Ошибка: объекты разные!");
    }
}
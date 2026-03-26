using Task2;
class Program
{
    static void Main()
    {
        LinkedListManager<int> manager = new LinkedListManager<int>();
        manager.AddLast(10);
        manager.AddLast(20);
        manager.AddFirst(5);
        manager.Display();

        try
        {
            if (manager.Contains(20))
                Console.WriteLine("20 найдено");
            if (manager.Remove(20))
                Console.WriteLine("20 удалено");
            manager.Display();
            if (manager.Remove(100))
                Console.WriteLine("100 удалено");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
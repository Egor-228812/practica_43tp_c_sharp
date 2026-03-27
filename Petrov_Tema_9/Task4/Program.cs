using Task4;
class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Введите путь для отслеживания: ");
            string path = Console.ReadLine();

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Указанная папка не существует.");
                return;
            }

            FileWatcher watcher = new FileWatcher(path);
            Console.WriteLine($"Начинаем отслеживание папки: {path}");
            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
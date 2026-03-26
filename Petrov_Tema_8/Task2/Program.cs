using Task3;
public interface ILogger<T>
{
    void Log(T message);
}
class Program
{
    static void Main()
    {
        try
        {
            ConsoleLogger<string> logger = new ConsoleLogger<string>();
            LoggerManager<string> manager = new LoggerManager<string>(logger);

            manager.LogError("Что-то случилось");
            manager.LogWarning("Проверьте ваш ввод");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
        }
    }
}
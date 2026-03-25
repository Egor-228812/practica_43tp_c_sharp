using Task2;

class Programm
{
    static void Main()
    {
        var handler = new RequestHandler();
        try
        {
            handler.ProcessRequest("/https://что-то-какой-то-запрос");
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"Перехвачено исключение: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
                Console.WriteLine($"Стек вызовов: {ex.InnerException.StackTrace}");
            }
        }
    }
}
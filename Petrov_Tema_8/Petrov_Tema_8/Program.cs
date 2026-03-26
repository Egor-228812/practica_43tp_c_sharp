using Task1;
class Programm
{
    static void Main()
    {
        ServiceRequestManager manager = new ServiceRequestManager();
        try
        {
            manager.AddRequest(new ServiceRequest(1, "толя", "Ремонт"));
            manager.AddRequest(new ServiceRequest(25, "ега", "планировка"));
            manager.AddRequest(new ServiceRequest(4, "мира", "Ремонт"));
            manager.AddRequest(new ServiceRequest(56, "саня", "консультация"));
            manager.AddRequest(new ServiceRequest(2, "ромчик", "настройка"));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении {ex.Message}");
        }
        manager.ShowRequest();
        try
        {
            var found = manager.FindRequest(4);
            Console.WriteLine($"Найдена заявка Id: {found.Id}, клиент: {found.CustomerName}, тип: {found.RequestType}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при поиске {ex.Message}");
        }
        try
        {
            manager.SortRequest();
            manager.ShowRequest();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ошибка при сортировке: {ex.Message}");
        }
        try
        {
            manager.RemoveRequest();
            manager.ShowRequest();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"обика при удалении: {ex.Message}");
        }
    }
}
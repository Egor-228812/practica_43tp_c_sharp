using Task1;
class Program
{
    static void Main()
    {
        try
        {
            UserFactory factory = new AdminFactory();
            IUser user = factory.CreateUser();
            Console.WriteLine("Админ: " + user.GetPermissions());

            factory = new ModeratorFactory();
            user = factory.CreateUser();
            Console.WriteLine("Модератор: " + user.GetPermissions());

            factory = new RegularFactory();
            user = factory.CreateUser();
            Console.WriteLine("Обычный пользователь: " + user.GetPermissions());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
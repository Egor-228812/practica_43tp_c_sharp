using Task3;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        var validator = new EmailValidator();

        Console.Write("Введите email: ");
        string input = Console.ReadLine();

        try
        {
            validator.ValidateEmail(input);
        }
        catch (InvalidEmailException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
    }
}
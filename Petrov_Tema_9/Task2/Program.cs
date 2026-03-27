using Task2;
class Program
{
    static void Main()
    {
        List<Person> people = new List<Person>
        {
            new Person("Майоров", 25),
            new Person("Петров", 18),
            new Person("Макарчук", 30),
            new Person("Швед", 12),
            new Person("Новиков", 18)
        };

        try
        {
            PersonFileSplitter splitter = new PersonFileSplitter();
            splitter.WritePeople(people);
            Console.WriteLine("Данные записаны в adults.data и minors.data");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи: {ex.Message}");
        }
    }
}
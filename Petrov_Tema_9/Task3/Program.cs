using Task3;
class Program
{
    static void Main()
    {
        try
        {
            PersonFileReader reader = new PersonFileReader();
            List<Person> allPeople = new List<Person>();

            if (File.Exists("adults.data"))
                allPeople.AddRange(reader.ReadPeople("adults.data"));
            if (File.Exists("minors.data"))
                allPeople.AddRange(reader.ReadPeople("minors.data"));

            PersonProcessor processor = new PersonProcessor();
            var grouped = processor.GroupByAge(allPeople);

            Console.WriteLine("Группировка по возрасту:");
            foreach (var group in grouped)
            {
                Console.WriteLine($"Возраст {group.Key}:");
                foreach (Person p in group.Value)
                    Console.WriteLine($"  {p.Name}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
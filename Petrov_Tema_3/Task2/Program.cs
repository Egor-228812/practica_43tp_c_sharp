Console.Write("Введите количество людей для генерации: ");
int size = Convert.ToInt32(Console.ReadLine());

Person[] people = PersonArrayHelper.GenerateRandomPersons(size);
Console.WriteLine("Сгенерированные люди:");
for (int i = 0; i < people.Length; i++)
    Console.WriteLine($"{people[i].Name}, {people[i].Age} лет");

PersonArrayHelper.SortByAge(people);
Console.WriteLine("\nОтсортировано по возрасту:");
for (int i = 0; i < people.Length; i++)
    Console.WriteLine($"{people[i].Name}, {people[i].Age} лет");

Console.Write("\nВведите минимальный возраст для фильтрации: ");
int minAge = Convert.ToInt32(Console.ReadLine());
Person[] filtered = PersonArrayHelper.FilterByMinAge(people, minAge);
Console.WriteLine($"Люди старше {minAge} лет:");
for (int i = 0; i < filtered.Length; i++)
    Console.WriteLine($"{filtered[i].Name}, {filtered[i].Age} лет");

var stats = PersonArrayHelper.GetAgeStatistics(people);
Console.WriteLine($"\nСтатистика возраста: " +
    $"средний = {stats.average:F1}," +
    $" минимальный = {stats.min}," +
    $" максимальный  = {stats.max}");
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age) { Name = name; Age = age; }
}

public static class PersonArrayHelper
{
    private static Random rand = new Random();
    private static string[] names = { "Саша", "Маша", "Дима", "Яна", "Егор", "Оля" };

    public static Person[] GenerateRandomPersons(int size)
    {
        Person[] persons = new Person[size];
        for (int i = 0; i < size; i++)
        {
            string name = names[rand.Next(names.Length)];
            int age = rand.Next(18, 71);
            persons[i] = new Person(name, age);
        }
        return persons;
    }

    public static void SortByAge(Person[] persons)
    {
        Array.Sort(persons, (p1, p2) => p1.Age.CompareTo(p2.Age));
    }

    public static Person[] FilterByMinAge(Person[] persons, int minAge)
    {
        int count = 0;
        for (int i = 0; i < persons.Length; i++)
            if (persons[i].Age >= minAge) count++;
        Person[] result = new Person[count];
        int index = 0;
        for (int i = 0; i < persons.Length; i++)
            if (persons[i].Age >= minAge) result[index++] = persons[i];
        return result;
    }

    public static (double average, int min, int max) GetAgeStatistics(Person[] persons)
    {
        double avg = 0;
        int min = int.MaxValue, max = int.MinValue;
        for (int i = 0; i < persons.Length; i++)
        {
            avg += persons[i].Age;
            if (persons[i].Age < min) min = persons[i].Age;
            if (persons[i].Age > max) max = persons[i].Age;
        }
        avg /= persons.Length;
        return (avg, min, max);
    }
}


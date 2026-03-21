int GetAge(DateTime birthDate)
{
    DateTime today = DateTime.Today;
    int age = today.Year - birthDate.Year;
    if (birthDate.Date > today.AddYears(-age))
        age--;
    return age < 0 ? 0 : age;
}

DateTime birthday = new DateTime(2007, 7, 13);
int age = GetAge(birthday);
Console.WriteLine($"Возраст: {age} лет");

string ReverseString(string str)
{
    if (string.IsNullOrEmpty(str) || str.Length == 1)
    return str;
    return ReverseString(str.Substring(1)) + str[0];
}

Console.Write("Введите строку: ");
string input = Console.ReadLine();
string reversed =ReverseString(input);
Console.WriteLine($"Перевернутая строка: {reversed}");
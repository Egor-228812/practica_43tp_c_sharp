string ToBinary(int number)
{
    if (number == 0) return "0";
    string binary = "";
    int n = number;
    while (n > 0)
    {
        binary = (n % 2) + binary;
        n /= 2;
    }
    return binary;
}

Console.Write("Введите целое число: ");
int num = Convert.ToInt32(Console.ReadLine());
string binary = ToBinary(num);
Console.WriteLine($"Двоичное представление: {binary}");
   
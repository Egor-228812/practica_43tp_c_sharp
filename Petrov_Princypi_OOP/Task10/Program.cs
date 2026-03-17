Console.WriteLine("Enter n:");
int n = Convert.ToInt16(Console.ReadLine());
while (n != 1)
{
    if (n % 2 == 0) n = n / 2;
    else n = 3 * n + 1;
    Console.WriteLine($"n: {n}");
}
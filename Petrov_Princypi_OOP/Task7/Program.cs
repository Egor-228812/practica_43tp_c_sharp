Console.WriteLine("Enter x:");
decimal x = Convert.ToDecimal(Console.ReadLine());
int count = 0;
while (count != 200)
{
    count += 10;
    Console.WriteLine($"{count} pieces of goods coast: {count * x}");
}
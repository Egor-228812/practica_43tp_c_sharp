Console.WriteLine("Enter A:");
double a = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Enter N:");
int n = Convert.ToInt16(Console.ReadLine());
double answer = 0;
for (int i = 0; i <= n; i++)
{
    Console.WriteLine($"{i}, {a}, {Math.Pow(a,i)}");
    answer += Math.Pow(a, i);
}
Console.WriteLine($"Answer: {answer}");

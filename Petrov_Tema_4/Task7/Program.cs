Console.WriteLine("Поочередно введите 2 целых числа");
int a = int.Parse(Console.ReadLine());
int b = int.Parse(Console.ReadLine());
Console.WriteLine($"Сумма 2 целых чисел: {Calculator.Calculate(a, b)}");
Console.WriteLine("Введите 2 вещественных числа: ");
double c = double.Parse(Console.ReadLine());
double d = double.Parse(Console.ReadLine());
Console.WriteLine($"Сумма 2 вещественных чисел: {Calculator.Calculate(d, c)}");
class Calculator
{
    public static int Calculate(int a, int b) { return a + b; }
    public static double Calculate(double a, double b) { return a + b; }
}


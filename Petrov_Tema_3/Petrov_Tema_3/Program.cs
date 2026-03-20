class AvarageArithmetic
{
    public double a;
    public double b;

    public AvarageArithmetic(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
    public double AvarageAr()
    {
        return (a + b) / 2;
    }
    public double Primer()
    {
        return (Math.Pow(b, 3) + Math.Sqrt(a));
    }
    public static void Main()
    {
        Console.WriteLine("Ввиедите 2 числа: ");
        int num1 = int.Parse(Console.ReadLine());
        int num2 = int.Parse(Console.ReadLine());
        AvarageArithmetic av = new AvarageArithmetic(num1, num2);
        Console.WriteLine($"Среднее арифметическое: {av.AvarageAr()}");
        Console.WriteLine($"Решение формулы: {av.Primer():F3}");
    }
}

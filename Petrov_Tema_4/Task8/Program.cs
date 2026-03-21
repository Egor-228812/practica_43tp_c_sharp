Console.WriteLine("ВВЕДИТЕ ЦЕЛОЕ ЧИСЛО И ИНКРЕМЕНТ: ");
int number = int.Parse(Console.ReadLine());
int incr = int.Parse(Console.ReadLine());
CIncrement.Increase(ref number, incr);
Console.WriteLine("Измененное число "+number);
Console.WriteLine("введте вещественное число и его инкремент: ");
double number_1 = double.Parse(Console.ReadLine());
double incr_1 = double.Parse(Console.ReadLine());
CIncrement.Increase(ref number_1, incr_1);
Console.WriteLine("Измененное число " + number_1);

class CIncrement
{
    public static void Increase(ref int value, int increment) { value+=increment; }
    public static void Increase(ref double value, double increment) { value+=increment; }
}
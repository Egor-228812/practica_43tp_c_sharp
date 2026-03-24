using Task1;
class Programm
{
    public delegate decimal DiscountCalculator(decimal price);
    static void Main()
    {
        Console.WriteLine("Введие цену товара: ");
        decimal price = decimal.Parse(Console.ReadLine());
        StudentDiscount sd = new StudentDiscount();
        SeniorDiscount snd = new SeniorDiscount();
        DiscountCalculator del = sd.StDiscount;
        del += snd.SnDiscount;
        Console.WriteLine($"Цена после всех скидок: {del(price):F2}"); ;
    }
}
using Task1;
class Programm
{
    static void Main()
    {
        Transport[] ts =
        {
            new Car(), new Bike(), new Airplane()
        };
        foreach (Transport t in ts)
        {
            Console.WriteLine(t.Move());
        }
    }
}
using Task3;

public interface IProgrammer
{
    void WriteCode();
}

public interface IDesigner
{
    void Design();
}


class Program
{
    static void Main()
    {
        ITSpecialist[] specialists = new ITSpecialist[]
        {
            new BackendDeveloper("Алексей"),
            new UXDesigner("Мария"),
            new BackendDeveloper("Дмитрий"),
            new UXDesigner("Елена")
        };

        Console.WriteLine("Программисты:");
        for (int i = 0; i < specialists.Length; i++)
        {
            if (specialists[i] is IProgrammer programmer)
            {
                programmer.WriteCode();
            }
        }
    }
}
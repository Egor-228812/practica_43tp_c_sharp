using Task3;
class Program
{
    static void Main()
    {
        try
        {
            BankAccountService service = new BankAccountService();
            BankingTerminal terminal = new BankingTerminal();

            ICommand transfer1 = new TransferMoneyCommand(service, 15000, "Иванов", "Петров");
            ICommand transfer2 = new TransferMoneyCommand(service, 5000, "Сидоров", "Козлова");

            terminal.ExecuteCommand(transfer1);
            terminal.ExecuteCommand(transfer2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
using System.Data;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите номер товара: ");
        int id = int.Parse(Console.ReadLine());
        ProcessOrder(id, ApproveOrder);
        ProcessOrder(id, CancelOrder);

    }
    public delegate void OrderProcessor(int orderID);
    public static void ProcessOrder(int orderID, OrderProcessor processor)
    {
        processor(orderID);
    }
    public static void ApproveOrder(int orderID)
    {
        Console.WriteLine($"Заказ {orderID} подтвержден");
    }
    public static void CancelOrder(int orderID)
    {
        Console.WriteLine($"Заказ {orderID} отменен");
    }
}
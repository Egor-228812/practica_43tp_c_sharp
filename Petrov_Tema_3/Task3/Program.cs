using System;


Product[] items = new Product[]
{
    new Electronics("Ноутбук", 50000, 3),
    new Electronics("Смартфон", 30000, 10),
    new Clothing("Футболка", 800, 50),
    new Clothing("Куртка", 3500, 15)
};

Warehouse warehouse = new Warehouse(items);

Console.WriteLine($"Общая стоимость товаров на складе: {warehouse.GetTotalStockValue()} руб.");
Product expensive = warehouse.FindMostExpensiveProduct();
Console.WriteLine($"Самый дорогой товар: {expensive.Name} по цене {expensive.Price} руб.");
public abstract class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}

public sealed class Electronics : Product
{
    public Electronics(string name, decimal price, int quantity) : base(name, price, quantity) { }
}

public sealed class Clothing : Product
{
    public Clothing(string name, decimal price, int quantity) : base(name, price, quantity) { }
}

public class Warehouse
{
    public Product[] Products { get; set; }
    public Warehouse(Product[] products) { Products = products; }

    public decimal GetTotalStockValue()
    {
        decimal total = 0;
        for (int i = 0; i < Products.Length; i++)
            total += Products[i].Price * Products[i].Quantity;
        return total;
    }

    public Product FindMostExpensiveProduct()
    {
        if (Products.Length == 0) return null;
        Product mostExpensive = Products[0];
        for (int i = 1; i < Products.Length; i++)
            if (Products[i].Price > mostExpensive.Price)
                mostExpensive = Products[i];
        return mostExpensive;
    }
}

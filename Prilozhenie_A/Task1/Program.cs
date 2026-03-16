using System;

Console.WriteLine("Вычисление стоимости покупки.");
Console.WriteLine("Введите исходные данные:");

Console.Write("Цена тетради: ");
double priceNotebook = Convert.ToDouble(Console.ReadLine());

Console.Write("Цена обложки");
double priceCover = Convert.ToDouble(Console.ReadLine());


Console.Write("Количество комплектов");
int count = Convert.ToInt16(Console.ReadLine());

double totalCost = (priceNotebook+ priceCover) * count;

Console.WriteLine($"Стоимость покупки: {totalCost:F2} руб.");

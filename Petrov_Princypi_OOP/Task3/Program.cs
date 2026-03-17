Console.WriteLine("Please, enter price");
double price = Convert.ToDouble(Console.ReadLine());
double i = 0;
while (i < 0.9)
{
    i += 0.1;
    Console.WriteLine($"Price of {i:F1} kg.: {price*i:F2}");
}


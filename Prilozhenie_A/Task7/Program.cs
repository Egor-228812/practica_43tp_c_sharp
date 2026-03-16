using System;

Console.WriteLine("Введите сопротивление R1:");
double r1 = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Введите сопротивление R2:");
double r2 = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Введите сопротивление R3:");
double r3 = Convert.ToDouble(Console.ReadLine());

double totalResistance = 1 / (1 / r1 + 1 / r2 + 1 / r3);

Console.WriteLine($"Общее сопротивление параллельного соединения: {totalResistance:F2} Ом");
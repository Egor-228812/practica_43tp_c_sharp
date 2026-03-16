using System;

Console.WriteLine("Введите угол в градусах:");
double angleDegrees = Convert.ToDouble(Console.ReadLine());

double angleRadians = angleDegrees * Math.PI / 180;

double z1 = (Math.Cos(angleRadians) + Math.Sin(angleRadians)) /
            (Math.Cos(angleRadians) - Math.Sin(angleRadians));

double z2 = Math.Tan(2 * angleRadians) + 1 / Math.Cos(2 * angleRadians);

Console.WriteLine($"Результат по первой формуле: {z1:F5}");
Console.WriteLine($"Результат по второй формуле: {z2:F5}");

using System;

Console.WriteLine("Tabulation of the function ctg(x) on the interval [π/4, π/2] with a step H = (B-A)/M, M=15");

double A = Math.PI / 4;
double B = Math.PI / 2;
int M = 15;
double H = (B - A) / M;

Console.WriteLine($"A = {A:F4}, B = {B:F4}, H = {H:F4}");
Console.WriteLine("i\tx\tctg(x)");

for (int i = 0; i <= M; i++)
{
    double x = A + i * H;
    double y = Math.Cos(x) / Math.Sin(x);
    Console.WriteLine($"{i}\t{x:F4}\t{y:F4}");
}
Console.ReadKey();
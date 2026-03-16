using System;

double x = 0.5;
double y = x * Math.Pow(Math.E,(x*x)) - (Math.Sin(Math.Sqrt(x))
    + Math.Pow(Math.Cos(Math.Log10(x)),2))/
    Math.Sqrt(Math.Abs( 1 - x * Math.PI));

Console.WriteLine("Результат: "+ y);
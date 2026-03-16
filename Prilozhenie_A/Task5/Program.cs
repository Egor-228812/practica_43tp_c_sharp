using System;

int a = 365;
int first = a / 100;
int second = (a - first * 100)/10;
int third = a % 10;
int b = third * 100 + first * 10 + second;
Console.WriteLine($"Начальное число: {a}");
Console.WriteLine($"Конечное число: {b}");
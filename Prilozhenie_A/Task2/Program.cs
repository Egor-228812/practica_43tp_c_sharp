using System;

Console.WriteLine("Введите двузначное число:");
int number = Convert.ToInt32(Console.ReadLine());
int tens = number / 10; 
int units = number % 10;

int reversed = units * 10 + tens;

Console.WriteLine($"Число после перестановки цифр: {reversed}");
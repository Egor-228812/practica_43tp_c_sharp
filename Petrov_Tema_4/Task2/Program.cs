void DigitCountSum(int K, out int C, out int S)
{
    C = 0;
    S = 0;
    int n = K;
    while (n > 0)
    {
        int digit = n % 10;
        S += digit;
        C++;
        n /= 10;
    }
    if (K == 0)
    {
        C = 1;
        S = 0;
    }
}

int[] numbers = { 123, 4567, 890, 5, 10001 };
foreach (int num in numbers)
{
    DigitCountSum(num, out int count, out int sum);
    Console.WriteLine($"Число: {num}, Количество цифр: {count}, Сумма цифр: {sum}");
}
    


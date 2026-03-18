Console.WriteLine("Enter n: ");
int n = Convert.ToInt16(Console.ReadLine());
int[,] arr = new int[n,n];
Console.WriteLine("Enter a: ");
int a = Convert.ToInt16(Console.ReadLine());
Console.WriteLine("Enter b: ");
int b = Convert.ToInt16(Console.ReadLine());
Console.WriteLine("Enter D: ");
int d = Convert.ToInt16(Console.ReadLine());
int count = 0;
Random r = new Random();
Console.WriteLine("Array: \n");
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        arr[i, j] = r.Next(a, b + 1);
        Console.Write(arr[i, j]+" ");
        if (arr[i, j] < d) count++;
    }
    Console.WriteLine();
}
double sr = 0;
for  (int i = 0;i < n; i++)
{
    for(int j = 0;j < n; j++) sr += arr[j, i];
    Console.WriteLine($"Arithmetic averages {i+1}: {sr/n}");
    sr = 0;
}
Console.WriteLine($"Count numder < D: {count}");
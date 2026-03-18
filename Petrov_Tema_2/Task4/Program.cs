int[,] arr = new int[23, 40];
Random rd = new Random();
Console.WriteLine("Sold tikets: ");
for (int i = 0; i < 23; i++)
{
    for (int j = 0; j < 40; j++)
    {
        arr[i, j] = rd.Next(0, 2);
        Console.Write(arr[i, j]+ " ");
    }
    Console.WriteLine();
}

for  (int j  = 0; j < 40; j++)
{
    if (arr[0, j] == 0) Console.WriteLine($"Sit number {j+1} available");
}
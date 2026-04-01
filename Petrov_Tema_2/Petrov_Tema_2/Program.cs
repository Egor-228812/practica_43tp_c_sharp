int[] arr = new int[5];
Random r = new Random();
Console.Write("Array: ");
int index = 0;
for (int i = 0; i < arr.Length; i++)
{
    arr[i] = r.Next(1,100);
    Console.Write(arr[i]+" ");
}
int min = arr[0];
for (int i = 0;i < arr.Length; i++)
{
    if (min >  arr[i])
    {
        min = arr[i];
        index = i;
    }
}
Console.WriteLine("\nMinimal number: "+min+" His number: "+index);

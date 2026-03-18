using System.Linq.Expressions;

static int Task(int[] arr, int k)
{
    int left = 0;
    int right = arr.Length - 1;
    while (left <= right)
    {
        int mid = left + (right - left) / 2; //чтобы не было переполнения
        if (arr[mid] == k) return mid;
        else if (arr[mid] < k) left = mid + 1;
        else right = mid - 1;
    }
    return -1;
}
Console.WriteLine("Enter n: ");
int n = Convert.ToInt16(Console.ReadLine());
int[] arr = new int[n];
Random r = new Random();
Console.WriteLine("Enter k: ");
int k = Convert.ToInt16(Console.ReadLine());
Console.Write("Array: ");
int sum = 0, count = 0, count_0 = 0;
// ну тут задание для варианта 
for (int i = 0; i < arr.Length; i++)
{
    arr[i] = r.Next(-100 , 100);
    Console.Write(arr[i] + " ");
    if (arr[i] > 0) sum += arr[i];
    else if (arr[i] < 0) count++;
    else count_0++;
}
Console.WriteLine();
//тут уже общее заданяи 
Array.Sort(arr);
Console.WriteLine("Sorted array: ");
for (int i = 0; i < arr.Length; i++) Console.Write(arr[i]+" ");
Console.WriteLine(Task(arr, k) == -1 ? "\nElement k not found"
    : "\nElement k is found");
Console.WriteLine($"Count zero: {count_0}" +
    $"\nCount negative: {count}" +
    $"\nSummary: {sum}");
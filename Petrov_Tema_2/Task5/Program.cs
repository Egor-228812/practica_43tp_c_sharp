using System;

Console.Write("Enter count rows: ");
int rows = Convert.ToInt32(Console.ReadLine());

double[][] array = new double[rows][];

for (int i = 0; i < rows; i++)
{
    Console.Write($"Enter count elements in row {i}: ");
    int cols = Convert.ToInt32(Console.ReadLine());
    array[i] = new double[cols];
    for (int j = 0; j < cols; j++)
    {
        Console.Write($"Element [{i},{j}]: ");
        array[i][j] = Convert.ToDouble(Console.ReadLine());
    }
}

Console.WriteLine("\nArray: ");
for (int i = 0; i < array.Length; i++)
{
    for (int j = 0; j < array[i].Length; j++)
    {
        Console.Write(array[i][j] + " ");
    }
    Console.WriteLine();
}
double[] averages = new double[rows];
for (int i = 0; i < rows; i++)
{
    double sum = 0;
    for (int j = 0; j < array[i].Length; j++)
    {
        sum += array[i][j];
    }
    averages[i] = array[i].Length > 0 ? sum / array[i].Length : 0;
}
double[][] newArray = new double[rows + 1][];
for (int i = 0; i < rows; i++)
{
    newArray[i] = array[i];
}
newArray[rows] = averages;

Console.WriteLine("\nArray after adding the row with arithmetic averages:");
for (int i = 0; i < newArray.Length; i++)
{
    for (int j = 0; j < newArray[i].Length; j++)
    {
        Console.Write(newArray[i][j] + " ");
    }
    Console.WriteLine();
}
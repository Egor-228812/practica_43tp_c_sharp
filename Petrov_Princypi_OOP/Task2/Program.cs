Console.WriteLine("Pleas, enter number: ");
int num = Convert.ToInt16(Console.ReadLine());
Console.WriteLine(((num % 2 != 0) && (num / 100 >= 1) && (num / 100 <10))
    ? "Данное целое число является нечетным трехзначным числом" : "false"); 

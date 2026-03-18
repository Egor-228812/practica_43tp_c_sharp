Console.Write("Enter string: ");
string input = Console.ReadLine();
char[] chars = input.ToCharArray();
Array.Sort(chars);
int oddCount = 0;
int count = 1;
for (int i = 1; i < chars.Length; i++)
{
    if (chars[i] == chars[i - 1])
    {
        count++;
    }
    else
    {
        if (count % 2 != 0) oddCount++;
        count = 1;
    }
}
if (chars.Length > 0 && count % 2 != 0) oddCount++;
if (oddCount <= 1)
    Console.WriteLine("We can have a palindrom");
else
    Console.WriteLine("we dont have a palindrom");
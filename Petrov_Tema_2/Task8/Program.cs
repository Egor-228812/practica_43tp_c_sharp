using System;
using System.Text;

Console.Write("Enter string for encrypt: ");
string input = Console.ReadLine();

Console.Write("Enter number: ");
int shift = Convert.ToInt32(Console.ReadLine());

string encrypted = CaesarEncrypt(input, shift);
Console.WriteLine($"Encrypted string: {encrypted}");

static string CaesarEncrypt(string text, int shift)
{
    StringBuilder result = new StringBuilder();

    foreach (char c in text)
    {
        if (char.IsLetter(c))
        {
            char baseChar = char.IsUpper(c) ? 'А' : 'а';
            int alphabetSize = 32; //без Ё
            int offset = ((c - baseChar + shift) % alphabetSize + alphabetSize) % alphabetSize;
            result.Append((char)(baseChar + offset));
        }
        else
        {
            result.Append(c);
        }
    }

    return result.ToString();
}
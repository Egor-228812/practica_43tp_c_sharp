using System.Text;

Console.WriteLine("Enter string: ");
string str = Console.ReadLine();
Console.WriteLine("Enter word for replace: ");
string word = Console.ReadLine();
string word_rp = "sosiski";
StringBuilder sb = new StringBuilder(str);
Console.WriteLine(sb.Replace(word, word_rp));
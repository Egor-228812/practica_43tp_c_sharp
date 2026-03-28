using Task2;
class Program
{
    static void Main()
    {
        TextFormatter formatter = new TextFormatter();
        string sample = "hello world! this is C#.";

        formatter.SetStrategy(new UpperCaseFormat());
        Console.WriteLine("Uppercase: " + formatter.FormatText(sample));

        formatter.SetStrategy(new LowerCaseFormat());
        Console.WriteLine("Lowercase: " + formatter.FormatText(sample));

        formatter.SetStrategy(new TitleCaseFormat());
        Console.WriteLine("Title Case: " + formatter.FormatText(sample));
    }
}
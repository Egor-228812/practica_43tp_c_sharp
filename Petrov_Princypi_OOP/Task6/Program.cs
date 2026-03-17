Console.WriteLine("Enter symbol m/w: ");
string gender = Console.ReadLine().Trim().ToLower();

if (gender == "m")
{
    Console.WriteLine("Mens names: Saha, Dima, Egor, Roma");
}
else if (gender == "w")
{
    Console.WriteLine("Womens names: Anna, Ekaterina, Maria, Olgf");
}
else
{
    Console.WriteLine("Uncorrect symbol");
}
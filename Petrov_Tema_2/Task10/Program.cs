using System;
using System.Text.RegularExpressions;

Console.Write("Enter IP-adres: ");
string ip = Console.ReadLine();

string pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
bool isValid = Regex.IsMatch(ip, pattern);

if (isValid)
    Console.WriteLine("String is IP-adres.");
else
    Console.WriteLine("String nit is IP-ardess.");
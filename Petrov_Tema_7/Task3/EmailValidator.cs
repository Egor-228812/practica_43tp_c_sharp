using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task3
{
    public class EmailValidator
    {
        public void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidEmailException("Email не может быть пустым.");
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
                throw new InvalidEmailException($"'{email}' — неверный формат email.");

            Console.WriteLine($"Email '{email}' корректен.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class BankAccountService
    {
        public void Transfer(decimal amount, string fromAccount, string toAccount)
        {
            Console.WriteLine($"Перевод {amount:C} со счёта {fromAccount} на счёт {toAccount}");
        }
    }   
}

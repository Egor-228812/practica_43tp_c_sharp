using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class TransferMoneyCommand : ICommand
    {
        private BankAccountService receiver;
        private decimal amount;
        private string fromAccount;
        private string toAccount;

        public TransferMoneyCommand(BankAccountService receiver, decimal amount, string fromAccount, string toAccount)
        {
            this.receiver = receiver;
            this.amount = amount;
            this.fromAccount = fromAccount;
            this.toAccount = toAccount;
        }

        public void Execute()
        {
            receiver.Transfer(amount, fromAccount, toAccount);
        }
    }
}

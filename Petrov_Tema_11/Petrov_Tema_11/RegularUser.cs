using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class RegularUser : IUser
    {
        public string GetPermissions() => "Только чтение и базовые действия";
    }
}

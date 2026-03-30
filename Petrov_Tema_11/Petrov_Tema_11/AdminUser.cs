using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class AdminUser : IUser
    {
        public string GetPermissions() => "Полный доступ ко всем функциям";
    }
}

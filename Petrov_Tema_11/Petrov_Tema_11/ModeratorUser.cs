using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class ModeratorUser : IUser
    {
        public string GetPermissions() => "Может модерировать контент и управлять пользователями";
    }
}

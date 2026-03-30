using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class ModeratorFactory : UserFactory
    {
        public override IUser CreateUser() => new ModeratorUser();
    }
}

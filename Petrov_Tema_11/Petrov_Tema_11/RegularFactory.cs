using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class RegularFactory : UserFactory
    {
        public override IUser CreateUser() => new RegularUser();
    }
}

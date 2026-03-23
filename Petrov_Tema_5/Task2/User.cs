using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class User
    {
        public Smartphone[] Smartphones { get; set; }

        public User(Smartphone[] phones)
        {
            Smartphones = phones;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Bike : Transport
    {
        public Bike() { Name = "Мотоцикл"; }
        public override string Move()
        {
            return $"{Name} находится в движении";
        }
    }
}

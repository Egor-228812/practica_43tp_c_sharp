using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Airplane : Transport
    {
        public Airplane() { Name = "Самолет"; }
        public override string Move()
        {
            return $"{Name} находится в движении";
        }
    }
}

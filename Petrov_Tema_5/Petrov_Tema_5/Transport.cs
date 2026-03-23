using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public abstract class Transport
    {
        public string Name { get; set; }
        public abstract string Move();
    }
}

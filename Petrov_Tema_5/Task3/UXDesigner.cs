using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class UXDesigner : ITSpecialist, IDesigner
    {
        public UXDesigner(string name) : base(name) { }
        public void Design() => Console.WriteLine($"{Name} создаёт дизайн интерфейсов");
    }

}

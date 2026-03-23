using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class BackendDeveloper : ITSpecialist, IProgrammer
    {
        public BackendDeveloper(string name) : base(name) { }
        public void WriteCode() => Console.WriteLine($"{Name} пишет код на C#");
    }

}

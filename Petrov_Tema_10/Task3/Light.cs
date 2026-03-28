using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Light : IDevice
    {
        public void Update(string state)
        {
            if (state == "Ночь")
                Console.WriteLine("Свет: выключен");
            else if (state == "День")
                Console.WriteLine("Свет: включён");
            else
                Console.WriteLine($"Свет: режим {state}");
        }
    }
}

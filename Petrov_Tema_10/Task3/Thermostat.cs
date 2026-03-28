using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Thermostat : IDevice
    {
        public void Update(string state)
        {
            if (state == "Ночь")
                Console.WriteLine("Термостат: температура 18°C");
            else if (state == "День")
                Console.WriteLine("Термостат: температура 22°C");
            else
                Console.WriteLine($"Термостат: режим {state}");
        }
    }
}

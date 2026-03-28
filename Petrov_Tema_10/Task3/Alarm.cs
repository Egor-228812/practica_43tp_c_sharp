using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Alarm : IDevice
    {
        public void Update(string state)
        {
            if (state == "Ночь")
                Console.WriteLine("Сигнализация: включена");
            else if (state == "День")
                Console.WriteLine("Сигнализация: выключена");
            else
                Console.WriteLine($"Сигнализация: режим {state}");
        }
    }
}

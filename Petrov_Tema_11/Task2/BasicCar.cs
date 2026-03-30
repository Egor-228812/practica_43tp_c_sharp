using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class BasicCar : ICar
    {
        public string GetFeatures() => "Автомобиль (базовая комплектация)";
        public double GetPrice() => 1000000;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class SunroofDecorator : CarDecorator
    {
        public SunroofDecorator(ICar car) : base(car) { }
        public override string GetFeatures() => car.GetFeatures() + ", люк на крыше";
        public override double GetPrice() => car.GetPrice() + 50000;
    }
}

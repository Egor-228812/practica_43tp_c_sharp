using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class LeatherSeatsDecorator : CarDecorator
    {
        public LeatherSeatsDecorator(ICar car) : base(car) { }
        public override string GetFeatures() => car.GetFeatures() + ", кожаные сиденья";
        public override double GetPrice() => car.GetPrice() + 70000;
    }
}

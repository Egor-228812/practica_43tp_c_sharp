using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class NavigationDecorator : CarDecorator
    {
        public NavigationDecorator(ICar car) : base(car) { }
        public override string GetFeatures() => car.GetFeatures() + ", навигационная система";
        public override double GetPrice() => car.GetPrice() + 30000;
    }
}

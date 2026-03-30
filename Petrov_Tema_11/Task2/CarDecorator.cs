using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public abstract class CarDecorator : ICar
    {
        protected ICar car;
        public CarDecorator(ICar car) => this.car = car;
        public abstract string GetFeatures();
        public abstract double GetPrice();
    }
}

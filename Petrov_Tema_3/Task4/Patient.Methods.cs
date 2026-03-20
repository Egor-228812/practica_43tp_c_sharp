using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task4
{
    public partial class Patient
    {
        public void DisplayInfo()
        {
            Console.WriteLine($"{Name}, {Age} лет, диагноз: {Diagnosis}");
        }
    }
}

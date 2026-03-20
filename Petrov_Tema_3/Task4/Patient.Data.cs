using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public partial class Patient
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Diagnosis { get; set; }

        public Patient(string name, int age, string diagnosis)
        {
            Name = name;
            Age = age;
            Diagnosis = diagnosis;
        }
    }
}

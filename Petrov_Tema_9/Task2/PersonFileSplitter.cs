using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class PersonFileSplitter
    {
        private string adultsFile = "adults.data";
        private string minorsFile = "minors.data";

        public void WritePeople(List<Person> people)
        {
            List<Person> adults = new List<Person>();
            List<Person> minors = new List<Person>();

            foreach (Person p in people)
            {
                if (p.Age >= 18)
                    adults.Add(p);
                else
                    minors.Add(p);
            }

            using (StreamWriter sw = new StreamWriter(adultsFile))
            {
                foreach (Person p in adults)
                    sw.WriteLine($"{p.Name},{p.Age}");
            }

            using (StreamWriter sw = new StreamWriter(minorsFile))
            {
                foreach (Person p in minors)
                    sw.WriteLine($"{p.Name},{p.Age}");
            }
        }
    }
}

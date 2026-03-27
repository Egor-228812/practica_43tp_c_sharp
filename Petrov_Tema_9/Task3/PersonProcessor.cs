using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class PersonProcessor
    {
        public Dictionary<int, List<Person>> GroupByAge(List<Person> people)
        {
            Dictionary<int, List<Person>> groups = new Dictionary<int, List<Person>>();
            foreach (Person p in people)
            {
                if (!groups.ContainsKey(p.Age))
                    groups[p.Age] = new List<Person>();
                groups[p.Age].Add(p);
            }
            return groups;
        }
    }
}

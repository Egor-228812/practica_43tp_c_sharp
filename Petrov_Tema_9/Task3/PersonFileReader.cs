using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class PersonFileReader
    {
        public List<Person> ReadPeople(string filePath)
        {
            List<Person> people = new List<Person>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int age))
                    {
                        people.Add(new Person(parts[0], age));
                    }
                }
            }
            return people;
        }
    }
}

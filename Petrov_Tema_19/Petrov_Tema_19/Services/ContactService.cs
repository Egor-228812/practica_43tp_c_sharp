using Petrov_Tema_19.Models;
using Petrov_Tema_19.Services;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Services
{
    public class ContactService : IContactService
    {
        private static List<Contact> _contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Петров Егор", PhoneNumber = "+375295404360", Email = "egor@example.com" },
            new Contact { Id = 2, Name = "Седеневсикй Мирослав", PhoneNumber = "+375292345678", Email = "mira@example.com" },
            new Contact { Id = 3, Name = "Швед Руслан", PhoneNumber = "+375293477789", Email = "rusia@example.com" },
            new Contact { Id = 4, Name = "Мосевич Артур", PhoneNumber = "+375299525789", Email = "artue@example.com" },
            new Contact { Id = 5, Name = "Макарчук Богдан", PhoneNumber = "+375293456159", Email = "bogdan@example.com" }
        };
        private static int _nextId = 4;

        public List<Contact> GetAll() => _contacts;

        public Contact GetById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

        public void Add(Contact contact)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
        }

        public List<Contact> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return _contacts;
            return _contacts.Where(c => c.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
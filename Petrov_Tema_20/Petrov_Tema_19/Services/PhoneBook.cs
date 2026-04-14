using Petrov_Tema_19.Models;
using System.Collections.Generic;

namespace Petrov_Tema_19.Services
{
    public interface IContactService
    {
        List<Contact> GetAll();
        Contact GetById(int id);
        void Add(Contact contact);
        List<Contact> SearchByName(string name);
    }
}
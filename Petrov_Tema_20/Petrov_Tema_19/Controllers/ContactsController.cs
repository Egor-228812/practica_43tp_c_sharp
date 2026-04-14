using Microsoft.AspNetCore.Mvc;
using Petrov_Tema_19.Models;
using Petrov_Tema_19.Services;


namespace Petrov_Tema_19.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // Главная страница со списком всех контактов
        public IActionResult Index()
        {
            var contacts = _contactService.GetAll();
            return View(contacts);
        }

        // Поиск по имени (маршрут /Contacts/Search/{name})
        [HttpGet]
        public IActionResult Search(string name)
        {
            var contacts = _contactService.SearchByName(name);
            return View("Index", contacts); // переиспользуем представление Index
        }

        // GET: форма добавления контакта
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: добавление нового контакта
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactService.Add(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
    }
}
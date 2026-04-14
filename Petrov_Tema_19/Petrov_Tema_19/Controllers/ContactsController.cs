using Microsoft.AspNetCore.Mvc;
using Petrov_Tema_19.Models;
using Petrov_Tema_19.Services;
using Petrov_Tema_19.ViewModels;

namespace Petrov_Tema_19.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // Главная страница со списком контактов
        public IActionResult Index(string searchName)
        {
            var contacts = string.IsNullOrEmpty(searchName)
                ? _contactService.GetAll()
                : _contactService.SearchByName(searchName);

            // Передаём сообщение через ViewBag (например, после добавления)
            if (TempData["SuccessMessage"] != null)
                ViewBag.Message = TempData["SuccessMessage"].ToString();

            return View(contacts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: добавление нового контакта
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact
                {
                    Name = viewModel.Name,
                    PhoneNumber = viewModel.PhoneNumber,
                    Email = viewModel.Email
                };
                _contactService.Add(contact);
                TempData["SuccessMessage"] = $"Контакт '{contact.Name}' успешно добавлен!";
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // Поиск через маршрут /Contacts/Search/{name}
        [HttpGet]
        public IActionResult Search(string name)
        {
            var contacts = _contactService.SearchByName(name);
            ViewBag.SearchQuery = name;
            return View("Index", contacts);
        }
    }
}
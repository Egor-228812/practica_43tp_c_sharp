using System.ComponentModel.DataAnnotations;

namespace Petrov_Tema_19.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [Phone(ErrorMessage = "Некорректный формат телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace app_class.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Имя", Prompt = "enter name")]
        [Required(ErrorMessage = "Имя обязательно для заполнения!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Укажите имя длинной от 2-ух до 100 символов")]
        public string Name { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст обязателен для заполнения!")]
        [Range(minimum: 1, maximum: 110, ErrorMessage = "Укажите возраст в промежутке от 1 до 110")]
        public int? Age { get; set; }

        [Display(Name = "Зарплата")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Укажите заработную плату от 0")]
        [Required(ErrorMessage = "Зарплата обязательна для заполнения!")]
        public decimal? Salary { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пароли не совпадают")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Укажите E-Mail адрес")]
        [EmailAddress(ErrorMessage = "Некорректный E-Mail адрес")]
        [Remote(action:"IsEmailUse", controller:"Home", ErrorMessage = "Email уже используется!")]
        public string Email { get; set; }
    }
}

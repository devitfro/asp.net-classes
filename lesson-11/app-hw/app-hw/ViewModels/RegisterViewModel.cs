using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace app_hw.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Firstname")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Укажите Firstname длинной от 3-х до 50 символов")]
        public string FirstName { get; set; } = "";

        [Required]
        [Display(Name = "Lastname")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Укажите Lastname длинной от 3-х до 50 символов")]
        public string LastName { get; set; } = "";

        [Required]
        [Range(18, 105, ErrorMessage = "Возраст должен быть от 18 до 105 лет.")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Введите корректный Email.")]
        public string Email { get; set; } = "";

        [Phone(ErrorMessage = "Введите корректный номер телефона.")]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; } = "";

        [StringLength(20, ErrorMessage = "Имя пользователя не должно превышать 20 символов.")]
        [RegularExpression(@"^\w+$", ErrorMessage = "Имя пользователя может содержать только буквы, цифры и подчеркивания.")]
        [Remote(action: "IsUsernameAvailable", controller: "Account", ErrorMessage = "Имя пользователя уже занято.")]
        public string Username { get; set; } = "";

        [CreditCard(ErrorMessage = "Введите корректный номер кредитной карты.")]
        public string? CreditCardNumber { get; set; }

        [Url(ErrorMessage = "Введите корректный URL-адрес.")]
        public string? Website { get; set; }

        [ValidateNever]
        public bool TermsOfService { get; set; } = false;
    }
}

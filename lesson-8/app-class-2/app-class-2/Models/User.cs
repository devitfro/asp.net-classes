using System.ComponentModel.DataAnnotations;

namespace app_class_2.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 20 символов")]
        public string Password { get; set; }

        [Range(18, 100, ErrorMessage = "Возраст должен быть от 18 до 100 лет")]
        public int Age { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace app_hw.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        public string? PhoneNumber { get; set; }

        [Required]
        public string Username { get; set; } = "";

        public int Age { get; set; }

        [Required]
        public string PasswordHash { get; set; } = "";

        public string? CreditCardNumber { get; set; }

        public string? Website { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace app_class_1.Models
{
    public record class User
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public int Age { get; set; }
        //public string? Language { get; set; }
        //public int CompanyId { get; set; }


        public int Id { get; set; } = 10;
        public string Name { get; set; }
        public int Age { get; set; }
        public string About { get; set; }
        public string Biography { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }

        [Display(Name = "Are you happy?")]
        public bool IsHappy { get; set; }
        public string Language { get; set; }
        public string FavoriteDish { get; set; }

        //[Display(Name = "Preferred working part?")]

        //public TimeOfDay TimeOfDay { get; set; }
    }
}

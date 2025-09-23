using System.ComponentModel.DataAnnotations;

namespace app_class.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title", Prompt = "Enter title")]
        [Required(ErrorMessage = "Name обязательно для заполнения!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Укажите Name длинной от 3-х до 50 символов")]
        public string Title { get; set; }

        [Display(Name = "Price", Prompt = "Enter Price")]
        [Required(ErrorMessage = "Price обязательно для заполнения!")]
        [Range(minimum: 1, maximum: 10000, ErrorMessage = "Укажите Price в промежутке от 1 до 10000")]
        public decimal Price { get; set; }


        [Display(Name = "Category", Prompt = "Enter Category")]
        [Required(ErrorMessage = "Category обязательно для заполнения!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Укажите Category длинной от 1-ого до 15 символов")]
        public string Category { get; set; }

        [Display(Name = "Description", Prompt = "Enter Description")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Укажите Description длинной от 10-и до 150 символов")]
        public string Description { get; set; }

        [Display(Name = "Manufacturer", Prompt = "Enter manufacturer")]
        [Required(ErrorMessage = "Manufacturer обязателен!")]
        public string Manufacturer { get; set; }

        [Display(Name = "Release Date", Prompt = "Enter release date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Release Date обязательна!")]
        public DateTime ReleaseDate { get; set; }



    }
}

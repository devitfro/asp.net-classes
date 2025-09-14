using System.ComponentModel.DataAnnotations;

namespace app_hw.ViewModels
{
    public class BookViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        public string? Genre { get; set; }

        [Range(1, 2100, ErrorMessage = "Year must be between 1 and 2100")]
        public int? Year { get; set; }
    }
}

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace app_hw.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        //public string Comment { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}

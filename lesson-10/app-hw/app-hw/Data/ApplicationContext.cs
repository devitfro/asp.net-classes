using app_hw.Models;
using Microsoft.EntityFrameworkCore;

namespace app_hw.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}

using asp_class_2.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_class_2.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }
    }
}

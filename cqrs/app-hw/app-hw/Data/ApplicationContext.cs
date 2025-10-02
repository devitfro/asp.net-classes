using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace app_hw.Data
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookHistory> BookHistories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<BookHistory>().HasKey(h => h.Id);
        }
    }
}

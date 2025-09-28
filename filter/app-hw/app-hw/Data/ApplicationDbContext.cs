using app_hw.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace app_hw.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Letter> Letters { get; set; }
    }
}

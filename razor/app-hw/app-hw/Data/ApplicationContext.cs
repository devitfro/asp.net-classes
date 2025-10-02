using app_hw.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace app_hw.Data
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<TodoItem> Todos { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
    }

}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace app_hw.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }

        public DbSet<ApplicationContext> Users { get; set; }
    }
}

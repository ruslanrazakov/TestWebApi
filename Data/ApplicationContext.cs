using System;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Models;

namespace TestWebApi.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<OrderModel> Orders { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<OrderModel>()
                .Property(e => e.SystemType)
                .HasConversion(
                    v => v.ToString(),
                    v => (SystemType)Enum.Parse(typeof(SystemType), v));
        }

    }
}
using ExportExcelDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExportExcelDemo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasData(new List<Person> {
                new Person { Id = 1, Name = "Felipe" },
                new Person { Id = 2, Name = "Claudia" },
                new Person { Id = 3, Name = "Roberto" }
                });

        }

        public DbSet<Person> People { get; set; }
    }

}

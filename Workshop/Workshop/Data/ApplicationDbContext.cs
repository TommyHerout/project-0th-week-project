using Microsoft.EntityFrameworkCore;
using Workshop.Models;
using Workshop.Models.Dto;

namespace Workshop.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowInfo> BorrowInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Person> Persons { get; set; }
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasOne(p => p.Person).WithMany(b => b.Books);
        }
    }
}
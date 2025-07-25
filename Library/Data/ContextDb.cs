using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class ContextDb(DbContextOptions<ContextDb> options) : DbContext(options)
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<User> User { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using Prozan_Mara_Lab2.Models;

namespace Prozan_Mara_Lab2.Data
{
    public class Prozan_Mara_Lab2Context : DbContext
    {
        public Prozan_Mara_Lab2Context(DbContextOptions<Prozan_Mara_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
    }
}
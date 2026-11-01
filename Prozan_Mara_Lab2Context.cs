using Microsoft.EntityFrameworkCore;
using Prozan_Mara_Lab2.Models;
using Prozan_Mara_Lab2;
namespace Prozan_Mara_Lab2
{
    public class Prozan_Mara_Lab2Context : DbContext
    {
        public DbSet<Prozan_Mara_Lab2.Models.Category> Category { get; set; } = default!;
        public Prozan_Mara_Lab2Context(DbContextOptions<Prozan_Mara_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;
    }
}
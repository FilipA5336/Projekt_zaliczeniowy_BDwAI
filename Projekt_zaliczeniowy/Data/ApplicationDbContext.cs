using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models;

namespace Projekt_zaliczeniowy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Zgloszenie> Zgloszenia { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Priorytet> Priorytety { get; set; }
        public DbSet<Komentarz> Komentarze { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Kategoria>().HasData(
                new Kategoria { Id = 1, Nazwa = "Frontend" },
                new Kategoria { Id = 2, Nazwa = "Backend" },
                new Kategoria { Id = 3, Nazwa = "Baza Danych" }
            );

            builder.Entity<Priorytet>().HasData(
                new Priorytet { Id = 1, Nazwa = "Niski" },
                new Priorytet { Id = 2, Nazwa = "Wysoki" },
                new Priorytet { Id = 3, Nazwa = "Krytyczny" }
            );
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P5_Express_Voitures_Identity.Models;

namespace P5_Express_Voitures_Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Voiture> Voitures { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Annonce> Annonces { get; set; }

        public DbSet<Reparation> Reparations { get; set; }

        public DbSet<Marge> Marges { get; set; }
    }
}

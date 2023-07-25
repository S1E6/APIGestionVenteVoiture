using apiGestionVente.Model;
using Microsoft.EntityFrameworkCore;

namespace apiGestionVente.Data
{
    public class GestionVenteContext : DbContext
    {
        public GestionVenteContext(DbContextOptions<GestionVenteContext> options) : base(options)
        {
        }

        public DbSet<Marque> Marques { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Acheter> Achats { get; set; }
        
        public DbSet<Commmande> Commandes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acheter>().HasKey(a => new { a.IDCLIENT, a.NUMSERIE });
            modelBuilder.Entity<Commmande>().HasKey(com => new { com.IDCLIENT, com.NUMSERIE });
            modelBuilder.Entity<Voiture>().HasKey(voiture => new { voiture.NUMSERIE });
            modelBuilder.Entity<Categorie>().HasKey(cat => new { cat.IDCATEGORIE});
            modelBuilder.Entity<Client>().HasKey(client => new { client.IDCLIENT });
            modelBuilder.Entity<Marque>().HasKey(mar => new { mar.IDMARQUE });

        }
    }
}
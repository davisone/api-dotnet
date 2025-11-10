using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess;

// BanqueDbContext hérite maintenant de IdentityDbContext pour utiliser ASP.NET Core Identity
// IdentityDbContext<Utilisateur, ApplicationRole, int> :
//   - Utilisateur : notre classe d'utilisateur personnalisée
//   - ApplicationRole : notre classe de rôle personnalisée
//   - int : le type de clé primaire pour Id
public class BanqueDbContext : IdentityDbContext<Utilisateur, ApplicationRole, int>
{
    public BanqueDbContext(DbContextOptions<BanqueDbContext> options) : base(options)
    {
    }

    public DbSet<Banque> Banques { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<CompteBancaire> CompteBancaires { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CompteBancaire>()
            .HasOne(c => c.Utilisateur)
            .WithMany(u => u.Comptes)
            .HasForeignKey(c => c.UtilisateurId);

        modelBuilder.Entity<CompteBancaire>()
            .HasOne(c => c.Banque)
            .WithMany(b => b.Comptes)
            .HasForeignKey(c => c.BanqueId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Compte)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CompteId);

        modelBuilder.Entity<CompteBancaire>()
            .Property(c => c.Solde)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Montant)
            .HasPrecision(18, 2);
    }
}
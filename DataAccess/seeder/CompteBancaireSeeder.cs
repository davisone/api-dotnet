using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess.seeder;

public class CompteBancaireSeeder
{
    private readonly BanqueDbContext _context;

    public CompteBancaireSeeder(BanqueDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (!await _context.CompteBancaires.AnyAsync())
        {
            var banques = await _context.Banques.ToListAsync();
            var utilisateurs = await _context.Utilisateurs.ToListAsync();

            if (banques.Count > 0 && utilisateurs.Count > 0)
            {
                var comptes = new List<CompteBancaire>
                {
                    new CompteBancaire
                    {
                        NumeroCompte = "FR7630001007941234567890185",
                        Solde = 5000.50m,
                        TypeCompte = "Compte Courant",
                        DateOuverture = DateTime.Now.AddYears(-2),
                        EstActif = true,
                        UtilisateurId = utilisateurs[0].Id,
                        BanqueId = banques[0].Id
                    },
                    new CompteBancaire
                    {
                        NumeroCompte = "FR7630001007941234567890186",
                        Solde = 15000.00m,
                        TypeCompte = "Compte Épargne",
                        DateOuverture = DateTime.Now.AddYears(-2),
                        EstActif = true,
                        UtilisateurId = utilisateurs[0].Id,
                        BanqueId = banques[0].Id
                    },
                    new CompteBancaire
                    {
                        NumeroCompte = "FR7630002007941234567890187",
                        Solde = 3200.75m,
                        TypeCompte = "Compte Courant",
                        DateOuverture = DateTime.Now.AddYears(-1),
                        EstActif = true,
                        UtilisateurId = utilisateurs[1].Id,
                        BanqueId = banques[1].Id
                    },
                    new CompteBancaire
                    {
                        NumeroCompte = "FR7630003007941234567890188",
                        Solde = 8500.25m,
                        TypeCompte = "Compte Courant",
                        DateOuverture = DateTime.Now.AddMonths(-6),
                        EstActif = true,
                        UtilisateurId = utilisateurs[2].Id,
                        BanqueId = banques[2].Id
                    },
                    new CompteBancaire
                    {
                        NumeroCompte = "FR7630001007941234567890189",
                        Solde = 1200.00m,
                        TypeCompte = "Compte Jeune",
                        DateOuverture = DateTime.Now.AddMonths(-3),
                        EstActif = true,
                        UtilisateurId = utilisateurs[3].Id,
                        BanqueId = banques[0].Id
                    }
                };

                await _context.CompteBancaires.AddRangeAsync(comptes);
                await _context.SaveChangesAsync();
            }
        }
    }
}
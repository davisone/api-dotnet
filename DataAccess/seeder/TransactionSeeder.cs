using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess.seeder;

public class TransactionSeeder
{
    private readonly BanqueDbContext _context;

    public TransactionSeeder(BanqueDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (!await _context.Transactions.AnyAsync())
        {
            var comptes = await _context.CompteBancaires.ToListAsync();

            if (comptes.Count > 0)
            {
                var transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Montant = -50.00m,
                        TypeTransaction = "Débit",
                        DateTransaction = DateTime.Now.AddDays(-10),
                        Description = "Achat supermarché",
                        CompteId = comptes[0].Id
                    },
                    new Transaction
                    {
                        Montant = -120.00m,
                        TypeTransaction = "Débit",
                        DateTransaction = DateTime.Now.AddDays(-8),
                        Description = "Facture électricité",
                        CompteId = comptes[0].Id
                    },
                    new Transaction
                    {
                        Montant = 2000.00m,
                        TypeTransaction = "Crédit",
                        DateTransaction = DateTime.Now.AddDays(-5),
                        Description = "Salaire mensuel",
                        CompteId = comptes[0].Id
                    },
                    new Transaction
                    {
                        Montant = -30.50m,
                        TypeTransaction = "Débit",
                        DateTransaction = DateTime.Now.AddDays(-3),
                        Description = "Restaurant",
                        CompteId = comptes[0].Id
                    },
                    new Transaction
                    {
                        Montant = 500.00m,
                        TypeTransaction = "Crédit",
                        DateTransaction = DateTime.Now.AddDays(-15),
                        Description = "Virement d'épargne",
                        CompteId = comptes[1].Id
                    },
                    new Transaction
                    {
                        Montant = -80.00m,
                        TypeTransaction = "Débit",
                        DateTransaction = DateTime.Now.AddDays(-7),
                        Description = "Abonnement internet",
                        CompteId = comptes[2].Id
                    },
                    new Transaction
                    {
                        Montant = 1500.00m,
                        TypeTransaction = "Crédit",
                        DateTransaction = DateTime.Now.AddDays(-2),
                        Description = "Virement",
                        CompteId = comptes[3].Id
                    },
                    new Transaction
                    {
                        Montant = -25.00m,
                        TypeTransaction = "Débit",
                        DateTransaction = DateTime.Now.AddDays(-1),
                        Description = "Cinéma",
                        CompteId = comptes[4].Id
                    }
                };

                await _context.Transactions.AddRangeAsync(transactions);
                await _context.SaveChangesAsync();
            }
        }
    }
}
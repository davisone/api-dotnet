using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess.seeder;

public class BanqueSeeder
{
    private readonly BanqueDbContext _context;

    public BanqueSeeder(BanqueDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (!await _context.Banques.AnyAsync())
        {
            var banques = new List<Banque>
            {
                new Banque
                {
                    Nom = "Banque Nationale de Paris",
                    Adresse = "16 Boulevard des Italiens, 75009 Paris",
                    CodeBanque = "BNP001"
                },
                new Banque
                {
                    Nom = "Crédit Agricole",
                    Adresse = "12 Place des États-Unis, 92120 Montrouge",
                    CodeBanque = "CA002"
                },
                new Banque
                {
                    Nom = "Société Générale",
                    Adresse = "29 Boulevard Haussmann, 75009 Paris",
                    CodeBanque = "SG003"
                }
            };

            await _context.Banques.AddRangeAsync(banques);
            await _context.SaveChangesAsync();
        }
    }
}
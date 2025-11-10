using Microsoft.AspNetCore.Identity;

namespace WebApplication1.DataAccess.seeder;

public class DbSeeder
{
    private readonly BanqueDbContext _context;
    private readonly UserManager<Utilisateur> _userManager;

    public DbSeeder(BanqueDbContext context, UserManager<Utilisateur> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        // Seed les banques
        var banqueSeeder = new BanqueSeeder(_context);
        await banqueSeeder.SeedAsync();

        // Seed les utilisateurs (nécessite UserManager pour créer les utilisateurs avec Identity)
        var utilisateurSeeder = new UtilisateurSeeder(_context, _userManager);
        await utilisateurSeeder.SeedAsync();

        // Seed les comptes bancaires (dépend des banques et utilisateurs)
        var compteBancaireSeeder = new CompteBancaireSeeder(_context);
        await compteBancaireSeeder.SeedAsync();

        // Seed les transactions (dépend des comptes bancaires)
        var transactionSeeder = new TransactionSeeder(_context);
        await transactionSeeder.SeedAsync();
    }
}
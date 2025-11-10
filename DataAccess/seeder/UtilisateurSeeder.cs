using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess.seeder;

public class UtilisateurSeeder
{
    private readonly BanqueDbContext _context;
    private readonly UserManager<Utilisateur> _userManager;

    public UtilisateurSeeder(BanqueDbContext context, UserManager<Utilisateur> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        if (!await _context.Utilisateurs.AnyAsync())
        {
            var utilisateurs = new[]
            {
                new { Nom = "Dupont", Prenom = "Jean", Email = "jean.dupont@email.com", Phone = "0601020304", Password = "Password123!", DateInscription = DateTime.Now.AddYears(-2) },
                new { Nom = "Martin", Prenom = "Sophie", Email = "sophie.martin@email.com", Phone = "0612345678", Password = "Password123!", DateInscription = DateTime.Now.AddYears(-1) },
                new { Nom = "Bernard", Prenom = "Pierre", Email = "pierre.bernard@email.com", Phone = "0623456789", Password = "Password123!", DateInscription = DateTime.Now.AddMonths(-6) },
                new { Nom = "Dubois", Prenom = "Marie", Email = "marie.dubois@email.com", Phone = "0634567890", Password = "Password123!", DateInscription = DateTime.Now.AddMonths(-3) }
            };

            foreach (var userData in utilisateurs)
            {
                var utilisateur = new Utilisateur
                {
                    UserName = userData.Email, // Identity requiert un UserName
                    Email = userData.Email,
                    Nom = userData.Nom,
                    Prenom = userData.Prenom,
                    PhoneNumber = userData.Phone, // Propriété héritée d'IdentityUser
                    DateInscription = userData.DateInscription,
                    EmailConfirmed = true // On confirme l'email directement pour les seeds
                };

                // UserManager gère automatiquement le hashage du mot de passe
                var result = await _userManager.CreateAsync(utilisateur, userData.Password);

                if (!result.Succeeded)
                {
                    // Log des erreurs si la création échoue
                    throw new Exception($"Erreur lors de la création de l'utilisateur {userData.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
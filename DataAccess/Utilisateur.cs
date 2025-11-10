using Microsoft.AspNetCore.Identity;

namespace WebApplication1.DataAccess;

// Utilisateur hérite de IdentityUser<int> pour utiliser Identity avec un Id de type int
// IdentityUser fournit : Id, UserName, Email, PasswordHash, PhoneNumber, etc.
public class Utilisateur : IdentityUser<int>
{
    // Propriétés personnalisées (en plus de celles d'IdentityUser)
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public DateTime DateInscription { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public List<CompteBancaire> Comptes { get; set; } = new();
}
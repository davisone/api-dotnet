using Microsoft.AspNetCore.Identity;

namespace WebApplication1.DataAccess;

// Classe de rôle personnalisée héritant de IdentityRole<int>
// Permet d'ajouter des propriétés personnalisées aux rôles si nécessaire
public class ApplicationRole : IdentityRole<int>
{
    // Ajoutez ici des propriétés personnalisées pour les rôles si nécessaire
    // Par exemple : Description, Permissions, etc.
}
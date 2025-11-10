using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

//  Objet pour la connexion :
// - Email, MotDePasse
public class LoginRequest
{
    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "L'email n'est pas valide")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis")]
    public required string MotDePasse { get; set; }
}
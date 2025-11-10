using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

//  Objet qui représente les données d'inscription :
//  - Nom, Prénom, Email, MotDePasse, NuméroTéléphone
//  - Contient les validations (email valide, mot de passe min 6 caractères)
public class RegisterRequest
{
    [Required(ErrorMessage = "Le nom est requis")]
    public required string Nom { get; set; }

    [Required(ErrorMessage = "Le prénom est requis")]
    public required string Prenom { get; set; }

    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "L'email n'est pas valide")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
    public required string MotDePasse { get; set; }

    [Required(ErrorMessage = "Le numéro de téléphone est requis")]
    [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide")]
    public required string NumeroTelephone { get; set; }
}
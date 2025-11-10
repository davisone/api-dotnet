using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

//Objet pour renouveler le token :
// - RefreshToken
public class RefreshTokenRequest
{
    [Required(ErrorMessage = "Le refresh token est requis")]
    public required string RefreshToken { get; set; }
}
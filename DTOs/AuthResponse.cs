namespace WebApplication1.DTOs;

public class AuthResponse
{
    //AccessToken (JWT pour accéder à l'API)
    public required string AccessToken { get; set; }
    //RefreshToken (pour renouveler le token)
    public required string RefreshToken { get; set; }
    public required string TokenType { get; set; } = "Bearer";
    //ExpiresIn (durée de validité en minutes)
    public int ExpiresIn { get; set; }
    //Utilisateur (infos de l'utilisateur connecté)
    public required UtilisateurDto Utilisateur { get; set; }
}

public class UtilisateurDto
{
    public int Id { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public required string Email { get; set; }
    public required string NumeroTelephone { get; set; }
}
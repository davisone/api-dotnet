using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.DTOs;

namespace WebApplication1.Service;

public class AuthService : IAuthService
{
    private readonly BanqueDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly UserManager<Utilisateur> _userManager;
    private readonly SignInManager<Utilisateur> _signInManager;

    public AuthService(
        BanqueDbContext context,
        IJwtService jwtService,
        UserManager<Utilisateur> userManager,
        SignInManager<Utilisateur> signInManager)
    {
        _context = context;
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    //Crée un nouvel utilisateur avec Identity UserManager
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // Créer le nouvel utilisateur
        var utilisateur = new Utilisateur
        {
            UserName = request.Email, // Identity requiert un UserName
            Email = request.Email,
            Nom = request.Nom,
            Prenom = request.Prenom,
            PhoneNumber = request.NumeroTelephone, // Propriété héritée d'IdentityUser
            DateInscription = DateTime.UtcNow,
            EmailConfirmed = true // On confirme l'email automatiquement
        };

        // UserManager gère automatiquement le hashage du mot de passe
        var result = await _userManager.CreateAsync(utilisateur, request.MotDePasse);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Échec de l'inscription: {errors}");
        }

        // Générer les tokens
        return await GenerateAuthResponse(utilisateur);
    }

    //Vérifie email/mot de passe avec Identity, génère les tokens
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        // Trouver l'utilisateur par email
        var utilisateur = await _userManager.FindByEmailAsync(request.Email);

        if (utilisateur == null)
        {
            throw new UnauthorizedAccessException("Email ou mot de passe incorrect");
        }

        // Vérifier le mot de passe avec SignInManager
        var result = await _signInManager.CheckPasswordSignInAsync(utilisateur, request.MotDePasse, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Email ou mot de passe incorrect");
        }

        // Générer les tokens
        return await GenerateAuthResponse(utilisateur);
    }

    //Renouvelle le token d'accès avec le refresh token
    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        // Trouver l'utilisateur avec ce refresh token
        var utilisateur = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        if (utilisateur == null)
        {
            throw new UnauthorizedAccessException("Refresh token invalide");
        }

        // Vérifier si le refresh token n'a pas expiré
        if (utilisateur.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token expiré");
        }

        //Génère les tokens JWT et les sauvegarde en base
        return await GenerateAuthResponse(utilisateur);
    }

    private async Task<AuthResponse> GenerateAuthResponse(Utilisateur utilisateur)
    {
        var accessToken = _jwtService.GenerateAccessToken(utilisateur);
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Sauvegarder le refresh token
        utilisateur.RefreshToken = refreshToken;
        utilisateur.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            TokenType = "Bearer",
            ExpiresIn = 60, // 60 minutes
            Utilisateur = new UtilisateurDto
            {
                Id = utilisateur.Id,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Email = utilisateur.Email ?? string.Empty,
                NumeroTelephone = utilisateur.PhoneNumber ?? string.Empty
            }
        };
    }
}
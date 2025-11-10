using System.Security.Claims;
using WebApplication1.DataAccess;

namespace WebApplication1.Service;

public interface IJwtService
{
    string GenerateAccessToken(Utilisateur utilisateur);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
using WebApplication1.DataAccess;

namespace WebApplication1.Service;

public interface IBanqueService
{
    Task<IEnumerable<Banque>> GetAllBanquesAsync();
    Task<Banque?> GetBanqueByIdAsync(int id);
    Task<Banque> CreateBanqueAsync(Banque banque);
    Task<Banque?> UpdateBanqueAsync(int id, Banque banque);
    Task<bool> DeleteBanqueAsync(int id);
    Task<bool> BanqueExistsAsync(string codeBanque);
}
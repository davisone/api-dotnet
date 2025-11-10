namespace WebApplication1.DataAccess;

public interface IBanqueRepository
{
    Task<IEnumerable<Banque>> GetAllBanquesAsync();
    Task<Banque?> GetBanqueByIdAsync(int id);
    Task<Banque> CreateBanqueAsync(Banque banque);
    Task<Banque?> UpdateBanqueAsync(int id, Banque banque);
    Task<bool> DeleteBanqueAsync(int id);
}
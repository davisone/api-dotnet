using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess;

public class BanqueRepository : IBanqueRepository
{
    private readonly BanqueDbContext _context;

    public BanqueRepository(BanqueDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Banque>> GetAllBanquesAsync()
    {
        return await _context.Banques
            .Include(b => b.Comptes)
            .ToListAsync();
    }

    public async Task<Banque?> GetBanqueByIdAsync(int id)
    {
        return await _context.Banques
            .Include(b => b.Comptes)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Banque> CreateBanqueAsync(Banque banque)
    {
        _context.Banques.Add(banque);
        await _context.SaveChangesAsync();
        return banque;
    }

    public async Task<Banque?> UpdateBanqueAsync(int id, Banque banque)
    {
        var existingBanque = await _context.Banques.FindAsync(id);
        if (existingBanque == null)
            return null;

        existingBanque.Nom = banque.Nom;
        existingBanque.Adresse = banque.Adresse;
        existingBanque.CodeBanque = banque.CodeBanque;

        await _context.SaveChangesAsync();
        return existingBanque;
    }

    public async Task<bool> DeleteBanqueAsync(int id)
    {
        var banque = await _context.Banques.FindAsync(id);
        if (banque == null)
            return false;

        _context.Banques.Remove(banque);
        await _context.SaveChangesAsync();
        return true;
    }
}
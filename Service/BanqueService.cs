using WebApplication1.DataAccess;

namespace WebApplication1.Service;

public class BanqueService : IBanqueService
{
    private readonly IBanqueRepository _repository;

    public BanqueService(IBanqueRepository repository)
    {
        _repository = repository;
    }

    //récupère toutes les banques
    public async Task<IEnumerable<Banque>> GetAllBanquesAsync()
    {
        return await _repository.GetAllBanquesAsync();
    }

    public async Task<Banque?> GetBanqueByIdAsync(int id)
    {
        return await _repository.GetBanqueByIdAsync(id);
    }

    //Création d'une nouvelle banque
    public async Task<Banque> CreateBanqueAsync(Banque banque)
    {
        if (string.IsNullOrWhiteSpace(banque.Nom))
            throw new ArgumentException("Le nom de la banque est requis");

        if (string.IsNullOrWhiteSpace(banque.CodeBanque))
            throw new ArgumentException("Le code banque est requis");

        return await _repository.CreateBanqueAsync(banque);
    }

    //MAJ d'une banque ?
    public async Task<Banque?> UpdateBanqueAsync(int id, Banque banque)
    {
        if (string.IsNullOrWhiteSpace(banque.Nom))
            throw new ArgumentException("Le nom de la banque est requis");

        if (string.IsNullOrWhiteSpace(banque.CodeBanque))
            throw new ArgumentException("Le code banque est requis");

        return await _repository.UpdateBanqueAsync(id, banque);
    }

    //pour supprimer une banque
    public async Task<bool> DeleteBanqueAsync(int id)
    {
        return await _repository.DeleteBanqueAsync(id);
    }

    public async Task<bool> BanqueExistsAsync(string codeBanque)
    {
        var banques = await _repository.GetAllBanquesAsync();
        return banques.Any(b => b.CodeBanque == codeBanque);
    }
}
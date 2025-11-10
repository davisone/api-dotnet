namespace WebApplication1.DataAccess;

public class CompteBancaire
{
    public int Id { get; set; }
    public required string NumeroCompte { get; set; }
    public decimal Solde { get; set; }
    public required string TypeCompte { get; set; }
    public DateTime DateOuverture { get; set; }
    public bool EstActif { get; set; }

    public int UtilisateurId { get; set; }
    public Utilisateur? Utilisateur { get; set; }

    public int BanqueId { get; set; }
    public Banque? Banque { get; set; }

    public List<Transaction> Transactions { get; set; } = new();
}
namespace WebApplication1.DataAccess;

public class Banque
{
    public int Id { get; set; }
    public required string Nom { get; set; }
    public required string Adresse { get; set; }
    public required string CodeBanque { get; set; }
    public List<CompteBancaire> Comptes { get; set; } = new();
}
namespace WebApplication1.DataAccess;

public class Transaction
{
    public int Id { get; set; }
    public decimal Montant { get; set; }
    public required string TypeTransaction { get; set; }
    public DateTime DateTransaction { get; set; }
    public required string Description { get; set; }

    public int CompteId { get; set; }
    public CompteBancaire? Compte { get; set; }
}
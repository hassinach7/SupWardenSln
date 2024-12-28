namespace SupWarden.Models.Models;

public class Element : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Identifiant { get; set; } = null!;
    public string? IdentifiantSup { get; set; }
    public string? Password { get; set; }
    public string Uri { get; set; } = null!;
    public string? Note { get; set; }
    public string? NoteSup { get; set; }
    public string? TypePassword { get; set; }
    public string? PieceJointe { get; set; }
    public bool IsSensible { get; set; }

    public string VaultId { get; set; } = null!;
    public virtual Vault? Vault { get; set; }
}
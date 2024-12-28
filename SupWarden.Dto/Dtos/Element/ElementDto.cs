using SupWarden.Dto.Dtos.Vault;
using SupWarden.Models.Enums;

namespace SupWarden.Dto.Dtos.Element;

public class ElementDto
{
    public string Id { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Name { get; set; } = null!;
    public string Identifiant { get; set; } = null!;
    public string? IdentifiantSup { get; set; }
    public string Password { get; set; } = null!;
    public string Uri { get; set; } = null!;
    public string? Note { get; set; }
    public string? NoteSup { get; set; }
    public string? TypePassword { get; set; }
    public string? PieceJointe { get; set; }
    public bool IsSensible { get; set; }
    public string VaultId { get; set; } = null!;
    public VaultDto Vault { get; set; } = null!;
    public PermissionLevel Permission { get; set; }
}

using SupWarden.Models.Enums;

namespace SupWarden.Models.Models;

public class Share 
{
    public string UserId { get; set; } = null!;
    public User? User { get; set; }

    public string VaultId { get; set; } = null!;
    public Vault? Vault { get; set; }

    public PermissionLevel Permission { get; set; }
}
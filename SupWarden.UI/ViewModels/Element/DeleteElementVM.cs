using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Element;

public class DeleteElementVM
{
    [Required]
    public string Id { get; set; } = null!;
    public string VaultName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Identifiant { get; set; } = null!;
    public string? IdentifiantSup { get; set; }
    public string? Uri { get; set; }
    public string? Note { get; set; }
    public string? NoteSup { get; set; }
    public bool IsSensible { get; set; } = true;
    public string? Password { get; set; }
    public string? TypePassword { get; set; }
}
using SupWarden.Ressource.Ressources.ElementRessources.Create;

namespace SupWarden.Ressource.Ressources.ElementRessources.Update;

public class UpdateElementRessource : CreateElementRessource
{
    public string? TypePassword { get; set; }
    public string? Password { get; set; }
    public string Id { get; set; } = null!;
}
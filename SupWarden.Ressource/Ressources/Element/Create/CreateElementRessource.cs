namespace SupWarden.Ressource.Ressources.ElementRessources.Create;

public class CreateElementRessource
{
    public string Name { get; set; } = null!;
    public string Identifiant { get; set; } = null!;
    public string IdentifiantSup { get; set; } = null!;
    public string Uri { get; set; } = null!;
    public string? Note { get; set; }
    public string? NoteSup { get; set; }
    public bool IsSensible { get; set; }
    public string VaultId { get; set; } = null!;// Foreign Key
}
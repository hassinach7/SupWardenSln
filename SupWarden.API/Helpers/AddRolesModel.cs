namespace SupWarden.API.Helpers;

public class AddRolesModel
{
    public List<string> Roles { get; set; } = new();
    public string UserId { get; set; } =null!;
}
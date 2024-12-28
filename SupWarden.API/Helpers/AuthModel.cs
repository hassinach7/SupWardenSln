namespace SupWarden.API.Helpers;

public class AuthModel
{
    public List<string> Messages { get; set; } = new();
    public bool IsAuthenticated { get; set; }
    public string? UserName { get; set; }
    public string Email { get; set; } = null!;
    public List<string> Roles { get; set; } = new();
    public string? Token { get; set; } = null!;
    public DateTime? ExpiredOn { get; set; }
    public string FullName { get; set; } = null!;
    public string? PinCode { get; set; }
}
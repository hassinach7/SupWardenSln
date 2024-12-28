namespace SupWarden.API.Helpers;

public class Helper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Helper(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "uid")?.Value;

        return userId ?? string.Empty;
    }
}

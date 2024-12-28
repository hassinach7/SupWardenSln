namespace SupWarden.UI.Middleware;

public class SessionAuthMiddlware
{
    private readonly RequestDelegate _next;

    public SessionAuthMiddlware(RequestDelegate next)
    {
        this._next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();
        if(!path!.StartsWith("/auth/login") && string.IsNullOrEmpty(context.Session.GetString("JWTToken")))
        {
            context.Response.Redirect("/auth/login");
            return;
        }
        await _next(context);
    }
}

public static class SessionAuthMiddlwareExtensions
{
    public static IApplicationBuilder useSessionAuthMiddlware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SessionAuthMiddlware>();
    }
}

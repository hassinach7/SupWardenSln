namespace SupWarden.API.Helpers;

public static  class SupWardenContainer
{
    public static IServiceCollection InjectionsServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<Helper>();
        services.AddTransient<IVaultService, VaultService>();
        services.AddTransient<IShareService, ShareService>();
        services.AddTransient<IGroupService, GroupService>();
        services.AddTransient<IElementService, ElementService>();
        services.AddTransient<IGroupeAssignmentsService, GroupeAssignmentsService>();

        return services;
    }
}

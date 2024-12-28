using SupWarden.UI.Middleware;
using SupWarden.UI.Models;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.Services.Implementations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.Configure<ApiSetting>(builder.Configuration.GetSection("ApiSetting"));

builder.Services.AddTransient<ApiSecurity>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IVaultService, VaultService>();
builder.Services.AddTransient<IElementService, ElementService>();
builder.Services.AddTransient<IShareService, ShareService>();
builder.Services.AddTransient<IGroupeService, GroupeService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.useSessionAuthMiddlware();

app.UseAuthorization();


app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
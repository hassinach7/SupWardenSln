using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupWarden.API.Helpers;

namespace SupWarden.API.Data;

public class SupWardenDbContext : IdentityDbContext<User>
{
    private readonly Helper helper;
    public DbSet<Element> Elements { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupeAssignment> GroupeAssignments { get; set; }
    public DbSet<Share> Shares { get; set; }
    public DbSet<Vault> Vaults { get; set; }

    public SupWardenDbContext(DbContextOptions<SupWardenDbContext> options, Helper helper) : base(options)
    {
        this.helper = helper;
        if (Database.GetPendingMigrations().Any())
        {
            Database.Migrate();
        }
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var identitySchema = "auth";
        builder.Entity<IdentityRole>().ToTable("Roles", identitySchema);
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", identitySchema);
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", identitySchema);
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", identitySchema);
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", identitySchema);
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", identitySchema);

        builder.ApplyConfiguration(new ElementConfiguration());
        builder.ApplyConfiguration(new GroupConfiguration());
        builder.ApplyConfiguration(new GroupeAssignmentConfiguration());
        builder.ApplyConfiguration(new ShareConfiguration());
        builder.ApplyConfiguration(new VaultConfiguration(helper));
        builder.ApplyConfiguration(new UserConfiguration());

        var guidR1 = "aafada04-cf40-4af8-a7db-c7f8a99dd6ab";
        var guidR2 = "b9c264ba-e65d-49bc-b7de-cdfc7397683b";
        var guidR3 = "73ffec6d-eb79-4fe9-b546-611ef40849a4";


        builder.Entity<IdentityRole>().HasData(
               new IdentityRole
               {
                   Name = "SampleUser",
                   NormalizedName = "SampleUser".ToUpper(),
                   Id =guidR1,
                   ConcurrencyStamp = guidR1
               },
               new IdentityRole
               {
                   Name = "Admin",
                   NormalizedName = "Admin".ToUpper(),
                   Id = guidR2,
                   ConcurrencyStamp = guidR2
               },
               new IdentityRole
               {
                   Name = "SuperAdmin",
                   NormalizedName = "SuperAdmin".ToUpper(),
                   Id = guidR3,
                   ConcurrencyStamp = guidR3
               }
        );
       

        foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
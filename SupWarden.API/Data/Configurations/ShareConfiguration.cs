using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupWarden.API.Data.Configurations;

public class ShareConfiguration : IEntityTypeConfiguration<Share>
{
    public void Configure(EntityTypeBuilder<Share> builder)
    {
        builder.HasKey(e => new { e.UserId, e.VaultId });
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.VaultId).IsRequired();
        builder.Property(e => e.Permission).IsRequired();
        builder.HasOne(e => e.User).WithMany(e => e.SharedVaults).HasForeignKey(e => e.UserId);
        builder.HasOne(e => e.Vault).WithMany(e => e.Shares).HasForeignKey(e => e.VaultId);

        builder.ToTable("Shares");
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupWarden.API.Data.Configurations;

public class ElementConfiguration : IEntityTypeConfiguration<Element>
{
    public void Configure(EntityTypeBuilder<Element> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Identifiant).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Password).HasMaxLength(100);
        builder.Property(e => e.Uri).HasMaxLength(250);
        builder.Property(e => e.Note).HasMaxLength(250);
        builder.Property(e => e.PieceJointe).HasMaxLength(250);
        builder.Property(e => e.IsSensible).IsRequired().HasDefaultValue(true);
        builder.Property(e => e.VaultId).IsRequired();

        builder.HasOne(e => e.Vault).WithMany(e => e.Elements)
            .HasForeignKey(e => e.VaultId)
            .HasPrincipalKey(e => e.Id);

        builder.ToTable("Elements");
    }
}
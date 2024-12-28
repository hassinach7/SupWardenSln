using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupWarden.API.Data.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.UserId).IsRequired();

        builder.HasOne(e => e.User).WithMany(e => e.Groups)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id);

        builder.HasMany(e => e.GroupeAssignments).WithOne(e => e.Group);

        builder.ToTable("Groups");
    }
}

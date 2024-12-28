using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupWarden.API.Data.Configurations;

public class GroupeAssignmentConfiguration : IEntityTypeConfiguration<GroupeAssignment>
{
    public void Configure(EntityTypeBuilder<GroupeAssignment> builder)
    {
        builder.HasKey(e => new { e.UserId, e.GroupId });

        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.GroupId).IsRequired();

        builder.HasOne(e => e.User).WithMany(e => e.GroupeAssignments).HasForeignKey(e => e.UserId);
        builder.HasOne(e => e.Group).WithMany(e => e.GroupeAssignments).HasForeignKey(e => e.GroupId);

        builder.ToTable("GroupeAssignments");
    }
}
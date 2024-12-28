
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupWarden.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.PinCode)
                   .HasMaxLength(10);

            builder.HasMany(e => e.Groups)
                   .WithOne(e => e.User)
                   .HasForeignKey(e => e.UserId);

            builder.HasMany(e => e.GroupeAssignments)
                   .WithOne(e => e.User)
                   .HasForeignKey(e => e.UserId);

            builder.HasMany(e => e.SharedVaults)
                   .WithOne(e => e.User)
                   .HasForeignKey(e => e.UserId);


            builder.HasMany(e => e.CreatedVaults)
                   .WithOne(e => e.Creator)
                   .HasForeignKey(e => e.UserId);

            builder.ToTable("Users");
        }

    }
}


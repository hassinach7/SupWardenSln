
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupWarden.API.Helpers;

namespace SupWarden.API.Data.Configurations
{
    public class VaultConfiguration : IEntityTypeConfiguration<Vault>
    {
        private readonly Helper _helper;

        public VaultConfiguration(Helper helper)
        {
            this._helper = helper;
        }
        public void Configure(EntityTypeBuilder<Vault> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.Label).IsRequired();

            builder.Property(e => e.IsPrivate)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.HasOne(e => e.Creator)
                 .WithMany(e => e.CreatedVaults) 
                 .HasForeignKey(v => v.UserId);


            builder.HasMany(e => e.Elements)
                   .WithOne(e => e.Vault)
                   .HasForeignKey(e => e.VaultId);


            builder.HasMany(e => e.Shares)
                   .WithOne(e => e.Vault)
                   .HasForeignKey(e => e.VaultId);


            //builder.HasQueryFilter(o => o.UserId == _helper.GetUserId());


            builder.ToTable("Vaults");

        }

    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using No1.Entities.CountryAggregate;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace No1.EntityFrameworkCore.Configurations.CountryConfiguration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(CountryConsts.NameLength).IsRequired();

        builder.HasMany(x => x.Cities).WithOne().HasForeignKey(x => x.CountryId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using No1.Entities.CountryAggregate;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace No1.EntityFrameworkCore.Configurations.CountryConfiguration;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(CityConsts.NameLength).IsRequired();
        
        builder.Property(x => x.PostCode).HasMaxLength(CityConsts.PostCodeLength).IsRequired();

        builder.Property(x => x.Location).IsRequired();
    }
}
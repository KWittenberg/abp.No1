using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using No1.Entities.CountryAggregate;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace No1.EntityFrameworkCore.Configurations.CountryConfiguration;

public class CityTranslationConfiguration : IEntityTypeConfiguration<CityTranslation>
{
    public void Configure(EntityTypeBuilder<CityTranslation> builder)
    {
        builder.ToTable("CityTranslations");

        builder.ConfigureByConvention();

        builder.Property(x => x.Name).HasMaxLength(CityConsts.NameLength).IsRequired();

        builder.HasKey(x => new { x.CityId, x.Language });
    }
}
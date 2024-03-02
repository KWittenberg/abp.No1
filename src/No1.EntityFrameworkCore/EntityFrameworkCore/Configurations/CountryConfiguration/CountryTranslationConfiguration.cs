using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using No1.Entities.CountryAggregate;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace No1.EntityFrameworkCore.Configurations.CountryConfiguration;

public class CountryTranslationConfiguration : IEntityTypeConfiguration<CountryTranslation>
{
    public void Configure(EntityTypeBuilder<CountryTranslation> builder)
    {
        builder.ToTable("CountryTranslations");

        builder.ConfigureByConvention();
        
        builder.Property(x => x.Name).HasMaxLength(CountryConsts.NameLength).IsRequired();

        builder.HasKey(x => new { x.CountryId, x.Language });
    }
}
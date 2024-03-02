using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using No1.Entities.LanguageAggregate;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace No1.EntityFrameworkCore.Configurations.LanguageConfiguration;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Languages");

        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(LanguageConsts.NameLength).IsRequired();

        builder.Property(x => x.Locale).HasMaxLength(LanguageConsts.LocaleLength).IsRequired();
    }
}
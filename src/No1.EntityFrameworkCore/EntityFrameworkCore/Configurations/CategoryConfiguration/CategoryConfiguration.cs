using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using No1.Entities.CategoryAggregate;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace No1.EntityFrameworkCore.Configurations.CategoryConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(CategoryConsts.NameLength).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(CategoryConsts.DescriptionLength);
    }
}
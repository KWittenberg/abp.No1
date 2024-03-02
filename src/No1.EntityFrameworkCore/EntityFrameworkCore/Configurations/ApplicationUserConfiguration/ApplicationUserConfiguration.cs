using Microsoft.EntityFrameworkCore.Metadata.Builders;
using No1.Entities.ApplicationUserAggregate;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users.EntityFrameworkCore;

namespace No1.EntityFrameworkCore.Configurations.ApplicationUserConfiguration;

public class ApplicationUserConfiguration //: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ConfigureByConvention();
        builder.ConfigureAbpUser();
    }
}
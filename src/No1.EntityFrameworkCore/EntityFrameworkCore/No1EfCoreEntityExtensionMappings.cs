using No1.Extensions;
using System;
using No1.Entities.ApplicationUserAggregate;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace No1.EntityFrameworkCore;

public static class No1EfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        No1GlobalFeatureConfigurator.Configure();
        No1ModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityUser, string>(IdentityUserExtensions.AvatarUrlProperty, (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasMaxLength(ApplicationUserConsts.AvatarUrlLength).IsRequired(false);
                })
                .MapEfCoreProperty<IdentityUser, DateTime?>(IdentityUserExtensions.DateOfBirth, (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.IsRequired(false);
                })
                .MapEfCoreProperty<IdentityUser, string>(IdentityUserExtensions.CountryProperty, (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasMaxLength(ApplicationUserConsts.CountryLength).IsRequired(false);
                })
                .MapEfCoreProperty<IdentityUser, string>(IdentityUserExtensions.PostCodeProperty, (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasMaxLength(ApplicationUserConsts.PostCodeLength).IsRequired(false);
                })
                .MapEfCoreProperty<IdentityUser, string>(IdentityUserExtensions.CityProperty, (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasMaxLength(ApplicationUserConsts.CityLength).IsRequired(false);
                })
                .MapEfCoreProperty<IdentityUser, string>(IdentityUserExtensions.StreetProperty, (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasMaxLength(ApplicationUserConsts.StreetLength).IsRequired(false);
                });


            /* You can configure extra properties for the
             * entities defined in the modules used by your application.
             *
             * This class can be used to map these extra properties to table fields in the database.
             *
             * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
             * USE No1ModuleExtensionConfigurator CLASS (in the Domain.Shared project)
             * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
             *
             * Example: Map a property to a table field:

                 ObjectExtensionManager.Instance
                     .MapEfCoreProperty<IdentityUser, string>(
                         "MyProperty",
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(128);
                         }
                     );

             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
             */
        });
    }
}
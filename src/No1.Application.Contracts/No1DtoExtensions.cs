using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace No1;

public static class No1DtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<IdentityUserDto, string>("AvatarUrl")
                .AddOrUpdateProperty<IdentityUserDto, string>("DateOfBirth")
                .AddOrUpdateProperty<IdentityUserDto, string>("Country")
                .AddOrUpdateProperty<IdentityUserDto, string>("Postcode")
                .AddOrUpdateProperty<IdentityUserDto, string>("City")
                .AddOrUpdateProperty<IdentityUserDto, string>("Street");
                
            
            /* You can add extension properties to DTOs
             * defined in the depended modules.
             *
             * Example:
             *
             * ObjectExtensionManager.Instance
             *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
             *
             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Object-Extensions
             */
        });
    }
}
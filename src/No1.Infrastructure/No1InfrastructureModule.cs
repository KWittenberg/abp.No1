using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using No1.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace No1;

[DependsOn(
    typeof(No1DomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpBlobStoringModule),
    typeof(AbpBlobStoringFileSystemModule)
)]
public class No1InfrastructureModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        No1EfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureDbContext();
        ConfigureMapper();
        // ConfigureBlobStorage();

        context.Services.AddAbpDbContext<No1DbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }

    private void ConfigureDbContext()
    {
        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also No1MigrationsDbContextFactory for EF Core tooling. */

            //options.UseSqlServer();
            options.UseSqlServer(o =>
            {
                //o.UseDateOnlyTimeOnly();
                o.UseNetTopologySuite();
            });
        });
    }

    private void ConfigureMapper()
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<No1InfrastructureModule>(); });
    }

    private void ConfigureBlobStorage()
    {
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseFileSystem(fileSystem => { fileSystem.BasePath = "../blob-storage"; });
            });
        });
    }
}
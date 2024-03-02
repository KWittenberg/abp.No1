using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using No1.Crm;
using No1.Interfaces;
using No1.OpenWeather;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace No1.EntityFrameworkCore;

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
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class No1EntityFrameworkCoreModule : AbpModule
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

        ConfigureCrmClient(context);
        ConfigureOpenWeatherClient(context);


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

            // options.UseSqlServer();
            options.UseSqlServer(o =>
            {
                //o.UseDateOnlyTimeOnly();
                o.UseNetTopologySuite();
            });
        });
    }

    private void ConfigureMapper()
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<No1EntityFrameworkCoreModule>(); });
    }

    private void ConfigureBlobStorage()
    {
        //Configure<AbpBlobStoringOptions>(options =>
        //{
        //    options.Containers.ConfigureDefault(container =>
        //    {
        //        container.UseFileSystem(fileSystem => { fileSystem.BasePath = "../blob-storage"; });
        //    });
        //});
    }

    private void ConfigureCrmClient(ServiceConfigurationContext context)
    {
        // Configure Based HttpClientFactory
        // context.Services.AddHttpClient();

        // Configure Named HttpClientFactory
        // context.Services.AddHttpClient("CrmClient", httpClient =>
        // {
        //    var app = ConfidentialClientApplicationBuilder.Create(_clientId).WithClientSecret(_clientSecret).WithAuthority(_accessTokenUrl).Build();
        //    var scopes = new[] { $"{_baseUrl}/.default" };
        //    var result = app.AcquireTokenForClient(scopes).ExecuteAsync().Result;

        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
        // });

        // Configure Typed HttpClientFactory
        // context.Services.AddHttpClient<ICrmClient, CrmClient>();

        // Configure Typed HttpClientFactory with CrmAuthenticationHandler
        context.Services.AddTransient<CrmAuthenticationHandler>();
        context.Services.AddHttpClient<ICrmClient, CrmClient>().AddHttpMessageHandler<CrmAuthenticationHandler>();
    }

    private void ConfigureOpenWeatherClient(ServiceConfigurationContext context)
    {
        // Configure Typed HttpClientFactory
        context.Services.AddHttpClient<IOpenWeatherClient, OpenWeatherClient>();
    }
}
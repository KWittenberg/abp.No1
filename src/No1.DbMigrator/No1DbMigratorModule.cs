using No1.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace No1.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(No1EntityFrameworkCoreModule),
    typeof(No1ApplicationContractsModule)
    )]
public class No1DbMigratorModule : AbpModule
{
}
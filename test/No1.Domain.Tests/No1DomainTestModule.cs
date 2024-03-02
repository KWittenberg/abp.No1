using No1.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace No1;

[DependsOn(
    typeof(No1EntityFrameworkCoreTestModule)
    )]
public class No1DomainTestModule : AbpModule
{

}

using Volo.Abp.Modularity;

namespace No1;

[DependsOn(
    typeof(No1ApplicationModule),
    typeof(No1DomainTestModule)
    )]
public class No1ApplicationTestModule : AbpModule
{

}

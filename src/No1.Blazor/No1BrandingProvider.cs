using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace No1.Blazor;

[Dependency(ReplaceServices = true)]
public class No1BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "No1";
}

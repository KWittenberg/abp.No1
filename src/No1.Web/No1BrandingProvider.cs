using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace No1.Web;

[Dependency(ReplaceServices = true)]
public class No1BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "No1";
}
using No1.Localization;
using Volo.Abp.AspNetCore.Components;

namespace No1.Blazor;

public abstract class No1ComponentBase : AbpComponentBase
{
    protected No1ComponentBase()
    {
        LocalizationResource = typeof(No1Resource);
    }
}

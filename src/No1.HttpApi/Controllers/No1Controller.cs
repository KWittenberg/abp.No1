using No1.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace No1.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class No1Controller : AbpControllerBase
{
    protected No1Controller()
    {
        LocalizationResource = typeof(No1Resource);
    }
}

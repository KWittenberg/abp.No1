using No1.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace No1.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class No1PageModel : AbpPageModel
{
    protected No1PageModel()
    {
        LocalizationResourceType = typeof(No1Resource);
    }
}

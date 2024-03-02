using No1.Localization;
using Volo.Abp.Application.Services;

namespace No1;

/* Inherit your application services from this class.
 */
public abstract class No1AppService : ApplicationService
{
    protected No1AppService()
    {
        LocalizationResource = typeof(No1Resource);
    }
}
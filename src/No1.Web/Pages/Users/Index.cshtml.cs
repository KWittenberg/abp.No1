using No1.ApplicationServices.ApplicationUserService;
using No1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace No1.Web.Pages.Users;

public class IndexModel : No1PageModel
{
    private readonly ApplicationUserAppService _applicationUserAppService;

    public IndexModel(ApplicationUserAppService applicationUserAppService)
    {
        _applicationUserAppService = applicationUserAppService;
    }

    public IList<ApplicationUserOutput>? AppUser { get; private set; }



    public async Task OnGetAsync()
    {
        if (CurrentUser is not null)
        {
            AppUser = await _applicationUserAppService.GetAllAsync();
        }
    }
}
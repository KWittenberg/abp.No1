using System.Threading.Tasks;

namespace No1.Web.Pages;

public class IndexModel : No1PageModel
{
    //private readonly ApplicationUserAppService _applicationUserAppService;

    //public IndexModel(ApplicationUserAppService applicationUserAppService)
    //{
    //    _applicationUserAppService = applicationUserAppService;
    //}

    //public ApplicationUserOutput? AppUser { get; private set; }

    public async Task OnGet()
    {
        //if (CurrentUser is not null)
        //{
        //    AppUser = await _applicationUserAppService.GetCurrentUser();
        //}
    }
}
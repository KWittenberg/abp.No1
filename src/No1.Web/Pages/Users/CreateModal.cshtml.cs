using Microsoft.AspNetCore.Mvc;
using No1.ApplicationServices.ApplicationUserService;
using No1.ApplicationServices.ApplicationUserService.Create;
using System.Threading.Tasks;

namespace No1.Web.Pages.Users;

public class CreateModalModel : No1PageModel
{
    private readonly ApplicationUserAppService _applicationUserAppService;

    public CreateModalModel(ApplicationUserAppService applicationUserAppService)
    {
        _applicationUserAppService = applicationUserAppService;
    }


    [BindProperty]
    public CreateApplicationUserInput ApplicationUser { get; set; }

    public async Task OnGetAsync()
    {
        ApplicationUser = new CreateApplicationUserInput();
    }

    public async Task OnPostAsync()
    {
        await _applicationUserAppService.CreateUserAsync(ApplicationUser);
    }
}
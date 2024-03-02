using Microsoft.AspNetCore.Mvc;
using No1.ApplicationServices.ApplicationUserService;
using No1.ApplicationServices.ApplicationUserService.Register;
using System.Threading.Tasks;

namespace No1.Web.Pages.Users;

public class RegisterModalModel : No1PageModel
{
    private readonly ApplicationUserAppService _applicationUserAppService;

    public RegisterModalModel(ApplicationUserAppService applicationUserAppService)
    {
        _applicationUserAppService = applicationUserAppService;
    }


    [BindProperty]
    public RegisterInput ApplicationUser { get; set; }

    public void OnGet()
    {
        ApplicationUser = new RegisterInput();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _applicationUserAppService.RegisterUserAsync(ApplicationUser);
            TempData["success"] = "User register successfully!";
        }
        else
        {
            TempData["error"] = "Failed to register user!";
        }
    }

    //public async Task<IActionResult> OnPostAsync()
    //{
    //    await _applicationUserAppService.RegisterUserAsync(ApplicationUser);
    //    return NoContent();
    //}
}
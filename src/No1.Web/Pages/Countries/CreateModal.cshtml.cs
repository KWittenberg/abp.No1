using Microsoft.AspNetCore.Mvc;
using No1.ApplicationServices.CountryService;
using No1.ApplicationServices.CountryService.CreateCountry;
using System.Threading.Tasks;

namespace No1.Web.Pages.Countries;

public class CreateModalModel : No1PageModel
{
    private readonly CountryAppService _countryAppService;

    public CreateModalModel(CountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
    }


    [BindProperty]
    public CreateCountryInput CreateCountry { get; set; }

    public void OnGet()
    {
        CreateCountry = new CreateCountryInput();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _countryAppService.CreateCountry(CreateCountry);
            TempData["success"] = $"{CreateCountry.Name} created successfully!";
        }
        else
        {
            TempData["error"] = $"{CreateCountry.Name} failed to create!";
        }
    }
}
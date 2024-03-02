using Microsoft.AspNetCore.Mvc;
using No1.ApplicationServices.CountryService;
using No1.ApplicationServices.CountryService.CreateCountry;
using System.Threading.Tasks;

namespace No1.Web.Pages.Countries;

public class AddCountry : No1PageModel
{
    private readonly CountryAppService _countryAppService;

    public AddCountry(CountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
    }

    [BindProperty]
    public string Name { get; set; }

    public virtual async Task<IActionResult> OnPost()
    {
        var input = new CreateCountryInput
        {
            Name = Name
        };

        if (ModelState.IsValid)
        {
            await _countryAppService.CreateCountry(input);
            TempData["success"] = "Country created successfully!";
        }

        return RedirectToPage("Index");
        //return RedirectToAction(nameof(Index));
    }
}
using Microsoft.AspNetCore.Mvc;
using No1.ApplicationServices.CountryService.CreateCountry;
using No1.ApplicationServices.CountryService;
using System.Threading.Tasks;
using System.Collections.Generic;
using No1.Models;
using System;

namespace No1.Web.Pages.Countries;

public class IndexModel : No1PageModel
{
    private readonly CountryAppService _countryAppService;
    private readonly CreateCountryInputValidator _createCountryInputValidator;

    public IndexModel(CountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
    }
    

    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public Guid CountryId { get; set; }
    
    public IList<CountryOutput> Countries { get; set; }




    public async Task OnGetAsync()
    {
        Countries = await _countryAppService.GetCountries();
    }
    
    //public async Task<IActionResult> OnGet()
    //{
    //    Countries = await _countryAppService.GetCountries();

    //    return Page();
    //}

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

        //return NoContent();
        return RedirectToPage("Index");
        //return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid CountryId)
    {
        await _countryAppService.DeleteCountry(CountryId);
        TempData["success"] = $"Country {CountryId} deleted successfully!";

        return RedirectToPage("Index");
    }

}
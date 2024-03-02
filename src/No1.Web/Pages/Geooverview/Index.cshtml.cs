using Microsoft.AspNetCore.Mvc;
using No1.ApplicationServices.CountryService;
using No1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace No1.Web.Pages.Geooverview;

public class IndexModel : No1PageModel
{
    private readonly CountryAppService _countryAppService;

    public IndexModel(CountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
    }


    [BindProperty]
    public string Name { get; set; }

    public IList<CountryOutput> Countries { get; set; }


    public async Task OnGetAsync()
    {
        Countries = await _countryAppService.GetCountries();
    }
}
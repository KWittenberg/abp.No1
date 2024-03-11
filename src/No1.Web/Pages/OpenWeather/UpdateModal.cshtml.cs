using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using No1.ApplicationServices.OpenWeatherService;
using No1.Enums;
using No1.Models;
using System.Threading.Tasks;

namespace No1.Web.Pages.OpenWeather;

public class UpdateModalModel(OpenWeatherAppService openWeather) : PageModel
{

    [BindProperty]
    public string CityName { get; set; }

    [BindProperty]
    public TemperatureUnit Unit { get; set; }

    public OpenWeatherOutput? OpenWeather { get; set; }


    public async Task<IActionResult> OnPostAsync()
    {
        OpenWeather = await openWeather.GetWeatherByCityName(CityName, Unit);
        return RedirectToPage("/OpenWeather/Index", new { CityName, Unit });
    }
}
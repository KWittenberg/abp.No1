using No1.ApplicationServices.OpenWeatherService;
using No1.Enums;
using No1.Models;
using System.Threading.Tasks;

namespace No1.Web.Pages.OpenWeather;

public class IndexModel(OpenWeatherAppService openWeather) : No1PageModel
{
    public OpenWeatherOutput? OpenWeather { get; set; }

    public async Task OnGetAsync(string cityName = "Požega, HR", TemperatureUnit unit = TemperatureUnit.Celsius)
    {
        OpenWeather = await openWeather.GetWeatherByCityName(cityName, unit);
    }
}
using No1.Enums;
using No1.Interfaces;
using No1.Models;
using System.Threading.Tasks;

namespace No1.ApplicationServices.OpenWeatherService;

public class OpenWeatherAppService(IOpenWeatherClient openWeatherClient) : No1AppService
{
    public async Task<OpenWeatherOutput?> GetWeatherByCityName(string cityName = "Požega, HR", TemperatureUnit unit = TemperatureUnit.Celsius)
    {
        return await openWeatherClient.GetWeatherByCityName(cityName, unit);
    }
}
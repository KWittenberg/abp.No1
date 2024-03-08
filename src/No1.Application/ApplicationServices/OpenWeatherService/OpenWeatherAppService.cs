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

    public async Task<OpenWeatherOutput?> GetWeatherByGeographicalCoordinates(double latitude = 45.3403, double longitude = 17.6853, TemperatureUnit unit = TemperatureUnit.Celsius)
    {
        return await openWeatherClient.GetWeatherByGeographicalCoordinates(latitude, longitude, unit);
    }
}
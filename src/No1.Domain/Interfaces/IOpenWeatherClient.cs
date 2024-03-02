using No1.Models;
using System.Threading.Tasks;
using No1.Enums;

namespace No1.Interfaces;

public interface IOpenWeatherClient
{
    Task<OpenWeatherOutput?> GetWeatherByCityName(string cityName, TemperatureUnit unit);
}
using Microsoft.Extensions.Options;
using No1.Enums;
using No1.Exceptions;
using No1.Interfaces;
using No1.Models;
using No1.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UnitsNet;
using Volo.Abp.DependencyInjection;

namespace No1.OpenWeather;

public class OpenWeatherClient(HttpClient client, IOptions<OpenWeatherOptions> weatherOptions) : IOpenWeatherClient, ITransientDependency
{
    private readonly OpenWeatherOptions _weatherOptions = weatherOptions.Value;


    public async Task<OpenWeatherOutput?> GetWeatherByCityName(string cityName, TemperatureUnit unit)
    {
        var response = await client.GetAsync($"{_weatherOptions.BaseUrl}?q={cityName}&appid={_weatherOptions.ApiKey}");
        if (!response.IsSuccessStatusCode) throw new InvalidResponseException(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        var dContent = JsonSerializer.Deserialize<OpenWeatherModel>(content);

        double temp, feelsLike, tempMin, tempMax;

        switch (unit)
        {
            case TemperatureUnit.Celsius:
                temp = ConvertKelvinToCelsius(dContent.main.temp);
                feelsLike = ConvertKelvinToCelsius(dContent.main.feels_like);
                tempMin = ConvertKelvinToCelsius(dContent.main.temp_min);
                tempMax = ConvertKelvinToCelsius(dContent.main.temp_max);
                break;
            case TemperatureUnit.Fahrenheit:
                temp = ConvertKelvinToFahrenheit(dContent.main.temp);
                feelsLike = ConvertKelvinToFahrenheit(dContent.main.feels_like);
                tempMin = ConvertKelvinToFahrenheit(dContent.main.temp_min);
                tempMax = ConvertKelvinToFahrenheit(dContent.main.temp_max);
                break;
            default:
                temp = dContent.main.temp;
                feelsLike = dContent.main.feels_like;
                tempMin = dContent.main.temp_min;
                tempMax = dContent.main.temp_max;
                break;
        }

        return new OpenWeatherOutput()
        {
            Id = dContent.id,
            Name = dContent.name,
            Longitude = dContent.coord.lon,
            Latitude = dContent.coord.lat,
            Timezone = dContent.timezone,
            Cod = dContent.cod,
            Temp = temp,
            FeelsLike = feelsLike,
            TempMin = tempMin,
            TempMax = tempMax,
            Pressure = dContent.main.pressure,
            Humidity = dContent.main.humidity,
            SeaLevel = dContent.main.sea_level,
            GrndLevel = dContent.main.grnd_level,
            WindSpeed = dContent.wind.speed,
            WindDeg = dContent.wind.deg,
            WindGust = dContent.wind.gust
        };
    }

    private double ConvertKelvinToCelsius(double kelvin)
    {
        return (Temperature.FromKelvins(kelvin)).DegreesCelsius;
    }

    private double ConvertKelvinToFahrenheit(double kelvin)
    {
        return (Temperature.FromKelvins(kelvin)).DegreesFahrenheit;
    }
}
using Microsoft.Extensions.Options;
using No1.Enums;
using No1.Exceptions;
using No1.Interfaces;
using No1.Models;
using No1.Options;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;


namespace No1.OpenWeather;

public class OpenWeatherClient(HttpClient client, IOptions<OpenWeatherOptions> weatherOptions, IObjectMapper mapper) : IOpenWeatherClient, ITransientDependency
{
    public async Task<OpenWeatherOutput?> GetWeatherByCityName(string cityName, TemperatureUnit unit)
    {
        var unitString = GetUnitString(unit);
        var dContent = await GetWeatherData($"weather?q={cityName}&units={unitString}");

        // return ProcessWeatherData(dContent);
        return mapper.Map<OpenWeatherModel, OpenWeatherOutput>(dContent);
    }

    public async Task<OpenWeatherOutput?> GetWeatherByGeographicalCoordinates(double latitude, double longitude, TemperatureUnit unit)
    {
        var unitString = GetUnitString(unit);
        var dContent = await GetWeatherData($"weather?lat={latitude}&lon={longitude}&units={unitString}");

        //return ProcessWeatherData(dContent);
        return mapper.Map<OpenWeatherModel, OpenWeatherOutput>(dContent);
    }


    private static string GetUnitString(TemperatureUnit unit)
    {
        return unit switch
        {
            TemperatureUnit.Celsius => "metric",
            TemperatureUnit.Fahrenheit => "imperial",
            TemperatureUnit.Kelvin => "standard",
            _ => "standard"
        };
    }

    private async Task<OpenWeatherModel?> GetWeatherData(string query)
    {
        var response = await client.GetAsync($"{weatherOptions.Value.BaseUrl}{query}&appid={weatherOptions.Value.ApiKey}");
        if (!response.IsSuccessStatusCode) throw new InvalidResponseException(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OpenWeatherModel>(content);
    }







    // Koristiti Maper ???
    private static OpenWeatherOutput ProcessWeatherData(OpenWeatherModel dContent)
    {
        return new OpenWeatherOutput()
        {
            Id = dContent.Id,
            Name = dContent.Name,
            Main = dContent.Weather.FirstOrDefault()?.Main,
            Description = dContent.Weather.FirstOrDefault()?.Description,
            Icon = dContent.Weather.FirstOrDefault()?.Icon,
            Longitude = dContent.Coord.Lon,
            Latitude = dContent.Coord.Lat,
            Timezone = dContent.Timezone,
            Cod = dContent.Cod,
            Temp = dContent.Main.Temp,
            FeelsLike = dContent.Main.FeelsLike,
            TempMin = dContent.Main.TempMin,
            TempMax = dContent.Main.TempMax,
            Pressure = dContent.Main.Pressure,
            Humidity = dContent.Main.Humidity,
            SeaLevel = dContent.Main.SeaLevel,
            GrndLevel = dContent.Main.GrndLevel,
            WindSpeed = dContent.Wind.Speed,
            WindDeg = dContent.Wind.Deg,
            WindGust = dContent.Wind.Gust
        };

        //return new OpenWeatherOutput()
        //{
        //    Id = dContent.id,
        //    Name = dContent.name,
        //    Main = dContent.weather.FirstOrDefault()?.main,
        //    Description = dContent.weather.FirstOrDefault()?.description,
        //    Icon = dContent.weather.FirstOrDefault()?.icon,
        //    Longitude = dContent.coord.lon,
        //    Latitude = dContent.coord.lat,
        //    Timezone = dContent.timezone,
        //    Cod = dContent.cod,
        //    Temp = dContent.main.temp,
        //    FeelsLike = dContent.main.feels_like,
        //    TempMin = dContent.main.temp_min,
        //    TempMax = dContent.main.temp_max,
        //    Pressure = dContent.main.pressure,
        //    Humidity = dContent.main.humidity,
        //    SeaLevel = dContent.main.sea_level,
        //    GrndLevel = dContent.main.grnd_level,
        //    WindSpeed = dContent.wind.speed,
        //    WindDeg = dContent.wind.deg,
        //    WindGust = dContent.wind.gust
        //};
    }


    //private OpenWeatherOutput ProcessWeatherData(OpenWeatherModel dContent, TemperatureUnit unit)
    //{
    //    double temp, feelsLike, tempMin, tempMax;

    //    switch (unit)
    //    {
    //        case TemperatureUnit.Celsius:
    //            temp = ConvertKelvinToCelsius(dContent.main.temp);
    //            feelsLike = ConvertKelvinToCelsius(dContent.main.feels_like);
    //            tempMin = ConvertKelvinToCelsius(dContent.main.temp_min);
    //            tempMax = ConvertKelvinToCelsius(dContent.main.temp_max);
    //            break;
    //        case TemperatureUnit.Fahrenheit:
    //            temp = ConvertKelvinToFahrenheit(dContent.main.temp);
    //            feelsLike = ConvertKelvinToFahrenheit(dContent.main.feels_like);
    //            tempMin = ConvertKelvinToFahrenheit(dContent.main.temp_min);
    //            tempMax = ConvertKelvinToFahrenheit(dContent.main.temp_max);
    //            break;
    //        default:
    //            temp = dContent.main.temp;
    //            feelsLike = dContent.main.feels_like;
    //            tempMin = dContent.main.temp_min;
    //            tempMax = dContent.main.temp_max;
    //            break;
    //    }

    //    return new OpenWeatherOutput()
    //    {
    //        Id = dContent.id,
    //        Name = dContent.name,
    //        Main = dContent.weather.FirstOrDefault()?.main,
    //        Description = dContent.weather.FirstOrDefault()?.description,
    //        Icon = dContent.weather.FirstOrDefault()?.icon,
    //        Longitude = dContent.coord.lon,
    //        Latitude = dContent.coord.lat,
    //        Timezone = dContent.timezone,
    //        Cod = dContent.cod,
    //        Temp = temp,
    //        FeelsLike = feelsLike,
    //        TempMin = tempMin,
    //        TempMax = tempMax,
    //        Pressure = dContent.main.pressure,
    //        Humidity = dContent.main.humidity,
    //        SeaLevel = dContent.main.sea_level,
    //        GrndLevel = dContent.main.grnd_level,
    //        WindSpeed = dContent.wind.speed,
    //        WindDeg = dContent.wind.deg,
    //        WindGust = dContent.wind.gust
    //    };
    //}

    //public async Task<OpenWeatherOutput?> GetWeatherByCityName(string cityName, TemperatureUnit unit)
    //{
    //    var response = await client.GetAsync($"{_weatherOptions.BaseUrl}weather?q={cityName}&appid={_weatherOptions.ApiKey}");
    //    if (!response.IsSuccessStatusCode) throw new InvalidResponseException(response.StatusCode.ToString());

    //    var content = await response.Content.ReadAsStringAsync();
    //    var dContent = JsonSerializer.Deserialize<OpenWeatherModel>(content);

    //    double temp, feelsLike, tempMin, tempMax;

    //    switch (unit)
    //    {
    //        case TemperatureUnit.Celsius:
    //            temp = ConvertKelvinToCelsius(dContent.main.temp);
    //            feelsLike = ConvertKelvinToCelsius(dContent.main.feels_like);
    //            tempMin = ConvertKelvinToCelsius(dContent.main.temp_min);
    //            tempMax = ConvertKelvinToCelsius(dContent.main.temp_max);
    //            break;
    //        case TemperatureUnit.Fahrenheit:
    //            temp = ConvertKelvinToFahrenheit(dContent.main.temp);
    //            feelsLike = ConvertKelvinToFahrenheit(dContent.main.feels_like);
    //            tempMin = ConvertKelvinToFahrenheit(dContent.main.temp_min);
    //            tempMax = ConvertKelvinToFahrenheit(dContent.main.temp_max);
    //            break;
    //        default:
    //            temp = dContent.main.temp;
    //            feelsLike = dContent.main.feels_like;
    //            tempMin = dContent.main.temp_min;
    //            tempMax = dContent.main.temp_max;
    //            break;
    //    }

    //    return new OpenWeatherOutput()
    //    {
    //        Id = dContent.id,
    //        Name = dContent.name,
    //        Main = dContent.weather.FirstOrDefault()?.main,
    //        Description = dContent.weather.FirstOrDefault()?.description,
    //        Icon = dContent.weather.FirstOrDefault()?.icon,
    //        Longitude = dContent.coord.lon,
    //        Latitude = dContent.coord.lat,
    //        Timezone = dContent.timezone,
    //        Cod = dContent.cod,
    //        Temp = temp,
    //        FeelsLike = feelsLike,
    //        TempMin = tempMin,
    //        TempMax = tempMax,
    //        Pressure = dContent.main.pressure,
    //        Humidity = dContent.main.humidity,
    //        SeaLevel = dContent.main.sea_level,
    //        GrndLevel = dContent.main.grnd_level,
    //        WindSpeed = dContent.wind.speed,
    //        WindDeg = dContent.wind.deg,
    //        WindGust = dContent.wind.gust
    //    };
    //}

    //public async Task<OpenWeatherOutput?> GetWeatherByGeographicalCoordinates(double latitude, double longitude, TemperatureUnit unit)
    //{
    //    var response = await client.GetAsync($"{_weatherOptions.BaseUrl}onecall?lat={latitude}&lon={longitude}&appid={_weatherOptions.ApiKey}");
    //    if (!response.IsSuccessStatusCode) throw new InvalidResponseException(response.StatusCode.ToString());

    //    var content = await response.Content.ReadAsStringAsync();
    //    var dContent = JsonSerializer.Deserialize<OpenWeatherModel>(content);

    //    double temp, feelsLike, tempMin, tempMax;

    //    switch (unit)
    //    {
    //        case TemperatureUnit.Celsius:
    //            temp = ConvertKelvinToCelsius(dContent.main.temp);
    //            feelsLike = ConvertKelvinToCelsius(dContent.main.feels_like);
    //            tempMin = ConvertKelvinToCelsius(dContent.main.temp_min);
    //            tempMax = ConvertKelvinToCelsius(dContent.main.temp_max);
    //            break;
    //        case TemperatureUnit.Fahrenheit:
    //            temp = ConvertKelvinToFahrenheit(dContent.main.temp);
    //            feelsLike = ConvertKelvinToFahrenheit(dContent.main.feels_like);
    //            tempMin = ConvertKelvinToFahrenheit(dContent.main.temp_min);
    //            tempMax = ConvertKelvinToFahrenheit(dContent.main.temp_max);
    //            break;
    //        default:
    //            temp = dContent.main.temp;
    //            feelsLike = dContent.main.feels_like;
    //            tempMin = dContent.main.temp_min;
    //            tempMax = dContent.main.temp_max;
    //            break;
    //    }

    //    return new OpenWeatherOutput()
    //    {
    //        Id = dContent.id,
    //        Name = dContent.name,
    //        Main = dContent.weather.FirstOrDefault()?.main,
    //        Description = dContent.weather.FirstOrDefault()?.description,
    //        Icon = dContent.weather.FirstOrDefault()?.icon,
    //        Longitude = dContent.coord.lon,
    //        Latitude = dContent.coord.lat,
    //        Timezone = dContent.timezone,
    //        Cod = dContent.cod,
    //        Temp = temp,
    //        FeelsLike = feelsLike,
    //        TempMin = tempMin,
    //        TempMax = tempMax,
    //        Pressure = dContent.main.pressure,
    //        Humidity = dContent.main.humidity,
    //        SeaLevel = dContent.main.sea_level,
    //        GrndLevel = dContent.main.grnd_level,
    //        WindSpeed = dContent.wind.speed,
    //        WindDeg = dContent.wind.deg,
    //        WindGust = dContent.wind.gust
    //    };
    //}


    //private double ConvertKelvinToCelsius(double kelvin)
    //{
    //    return (Temperature.FromKelvins(kelvin)).DegreesCelsius;
    //}

    //private double ConvertKelvinToFahrenheit(double kelvin)
    //{
    //    return (Temperature.FromKelvins(kelvin)).DegreesFahrenheit;
    //}
}
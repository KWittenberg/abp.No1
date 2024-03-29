﻿using Microsoft.AspNetCore.Mvc;
using No1.ApplicationServices.OpenWeatherService;
using No1.Enums;
using No1.Models;
using System.Threading.Tasks;

namespace No1.Web.Pages.OpenWeather;

public class IndexModel(OpenWeatherAppService openWeather) : No1PageModel
{
    [BindProperty]
    public string CityName { get; set; }

    [BindProperty]
    public TemperatureUnit Unit { get; set; }

    public OpenWeatherOutput? OpenWeather { get; set; }


    public async Task OnGetAsync(string CityName = "Požega, HR", TemperatureUnit Unit = TemperatureUnit.Celsius)
    {
        OpenWeather = await openWeather.GetWeatherByCityName(CityName, Unit);
    }
}
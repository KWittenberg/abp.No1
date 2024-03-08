namespace No1.Models;

public class OpenWeatherOutput
{
    public int Id { get; set; }
    public string? Name { get; set; }

    // Weather
    public string? Main { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }

    // cord
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public int Timezone { get; set; }
    public int Cod { get; set; }

    // main
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public double TempMin { get; set; }
    public double TempMax { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public int SeaLevel { get; set; }
    public int GrndLevel { get; set; }

    // wind
    public double WindSpeed { get; set; }
    public int WindDeg { get; set; }
    public double WindGust { get; set; }
}
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace No1.Models;

public class OpenWeatherModel
{
    [JsonPropertyName("coord")]
    public Coord Coord { get; set; }

    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; }

    [JsonPropertyName("base")]
    public string Base { get; set; }

    [JsonPropertyName("main")]
    public Main Main { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; }

    [JsonPropertyName("clouds")]
    public Clouds Clouds { get; set; }

    [JsonPropertyName("dt")]
    public int Dt { get; set; }

    [JsonPropertyName("sys")]
    public Sys Sys { get; set; }

    [JsonPropertyName("timezone")]
    public int Timezone { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("cod")]
    public int Cod { get; set; }
}

public class Coord
{
    [JsonPropertyName("lon")]
    public double Lon { get; set; }

    [JsonPropertyName("lat")]
    public double Lat { get; set; }
}

public class Weather
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("main")]
    public string Main { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }
}

public class Main
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }

    [JsonPropertyName("temp_min")]
    public double TempMin { get; set; }

    [JsonPropertyName("temp_max")]
    public double TempMax { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("sea_level")]
    public int SeaLevel { get; set; }

    [JsonPropertyName("grnd_level")]
    public int GrndLevel { get; set; }
}

public class Clouds
{
    [JsonPropertyName("all")]
    public int All { get; set; }
}

public class Wind
{
    [JsonPropertyName("speed")]
    public double Speed { get; set; }

    [JsonPropertyName("deg")]
    public int Deg { get; set; }

    [JsonPropertyName("gust")]
    public double Gust { get; set; }
}

public class Sys
{
    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("sunrise")]
    public int Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public int Sunset { get; set; }
}




//public class OpenWeatherModel
//{
//    public Coord coord { get; set; }
//    public List<Weather> weather { get; set; }
//    public string @base { get; set; }
//    public Main main { get; set; }
//    public int visibility { get; set; }
//    public Wind wind { get; set; }
//    public Clouds clouds { get; set; }
//    public int dt { get; set; }
//    public Sys sys { get; set; }
//    public int timezone { get; set; }
//    public int id { get; set; }
//    public string name { get; set; }
//    public int cod { get; set; }
//}

//public class Main
//{
//    public double temp { get; set; }
//    public double feels_like { get; set; }
//    public double temp_min { get; set; }
//    public double temp_max { get; set; }
//    public int pressure { get; set; }
//    public int humidity { get; set; }
//    public int sea_level { get; set; }
//    public int grnd_level { get; set; }
//}

//public class Coord
//{
//    public double lon { get; set; }
//    public double lat { get; set; }
//}

//public class Clouds
//{
//    public int all { get; set; }
//}

//public class Sys
//{
//    public int type { get; set; }
//    public int id { get; set; }
//    public string country { get; set; }
//    public int sunrise { get; set; }
//    public int sunset { get; set; }
//}

//public class Weather
//{
//    public int id { get; set; }
//    public string main { get; set; }
//    public string description { get; set; }
//    public string icon { get; set; }
//}

//public class Wind
//{
//    public double speed { get; set; }
//    public int deg { get; set; }
//    public double gust { get; set; }
//}
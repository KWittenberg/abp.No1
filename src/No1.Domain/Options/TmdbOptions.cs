namespace No1.Options;

public class TmdbOptions
{
    public const string Section = "Tmdb";


    public string AccessTokenAuth { get; set; }

    public string ApiKey { get; set; }

    public string BaseUrl { get; set; }
}
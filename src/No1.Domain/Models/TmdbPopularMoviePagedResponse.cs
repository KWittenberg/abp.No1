using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace No1.Models;

public class TmdbPopularMoviePagedResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public IEnumerable<TmdbPopularMovie> Results { get; set; } = [];

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}
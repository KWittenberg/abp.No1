using Microsoft.Extensions.Options;
using No1.Exceptions;
using No1.Interfaces;
using No1.Models;
using No1.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace No1.Tmdb;

public class TmdbClient(HttpClient client, IOptions<TmdbOptions> tmdb) : ITmdbClient, ITransientDependency
{
    public async Task<TmdbPopularMoviePagedResponse?> GetPopularMoviesAsync(int page = 1)
    {
        //if (page < 1) page = 1;
        //if (page > 500) page = 500;

        //return await client.GetFromJsonAsync<TmdbPopularMoviePagedResponse>($"{tmdb.Value.BaseUrl}movie/popular?language=en-US&page={page}");

        var response = await client.GetAsync($"{tmdb.Value.BaseUrl}movie/popular?language=en-US&page={page}");
        if (!response.IsSuccessStatusCode) throw new InvalidResponseException(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TmdbPopularMoviePagedResponse>(content);
    }

    public async Task<TmdbMovieDetails?> GetMovieDetailsAsync(int id)
    {
        return await client.GetFromJsonAsync<TmdbMovieDetails>($"{tmdb.Value.BaseUrl}movie/{id}?api_key={tmdb.Value.ApiKey}");
    }
}
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
    public async Task<TmdbPopularMoviePagedResponse?> GetPopularMoviesAsync(int page)
    {
        var response = await client.GetAsync($"{tmdb.Value.BaseUrl}movie/popular?language=en-US&page={page}");
        if (!response.IsSuccessStatusCode) await HandleErrorResponse(response);

        return await response.Content.ReadFromJsonAsync<TmdbPopularMoviePagedResponse>();
    }

    private static async Task HandleErrorResponse(HttpResponseMessage response)
    {
        var dContent = await response.Content.ReadFromJsonAsync<TmdbErrorModel>();
        if (dContent is null) throw new InvalidResponseException();

        throw new InvalidResponseException($"Code: {dContent.StatusCode} | Success: {dContent.Success}", dContent);
    }

    private static async Task HandleErrorResponseV1(HttpResponseMessage response)
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        var dContent = JsonSerializer.Deserialize<TmdbErrorModel>(errorContent);
        if (dContent is null) throw new InvalidResponseException();

        throw new InvalidResponseException($"Code: {dContent.StatusCode} | Success: {dContent.Success}", dContent);
    }


    public async Task<TmdbMovieDetails?> GetMovieDetailsAsync(int id)
    {
        return await client.GetFromJsonAsync<TmdbMovieDetails>($"{tmdb.Value.BaseUrl}movie/{id}?api_key={tmdb.Value.ApiKey}");
    }

    public async Task<TmdbPopularMoviePagedResponse?> GetPopularMoviesAsyncV1(int page)
    {
        if (page < 1) page = 1;
        if (page > 500) page = 500;

        return await client.GetFromJsonAsync<TmdbPopularMoviePagedResponse>($"{tmdb.Value.BaseUrl}movie/popular?language=en-US&page={page}");
    }

    public async Task<TmdbPopularMoviePagedResponse?> GetPopularMoviesAsyncV2(int page)
    {
        var response = await client.GetAsync($"{tmdb.Value.BaseUrl}movie/popular?language=en-US&page={page}");
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var dContent = JsonSerializer.Deserialize<TmdbErrorModel>(errorContent);
            if (dContent is null) throw new InvalidResponseException();

            throw new InvalidResponseException($"Code: {dContent.StatusCode} | Success: {dContent.Success}", dContent);
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TmdbPopularMoviePagedResponse>(content);
    }

    public async Task<TmdbPopularMoviePagedResponse?> GetPopularMoviesAsyncV3(int page)
    {
        var response = await client.GetAsync($"{tmdb.Value.BaseUrl}movie/popular?language=en-US&page={page}");
        if (!response.IsSuccessStatusCode) HandleErrorResponse(response);

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TmdbPopularMoviePagedResponse>(content);
    }
}
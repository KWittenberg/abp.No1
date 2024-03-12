using No1.Interfaces;
using No1.Models;
using System.Threading.Tasks;

namespace No1.ApplicationServices.TmdbService;

public class TmdbAppService(ITmdbClient tmdbClient) : No1AppService
{
    public async Task<TmdbPopularMoviePagedResponse?> GetPopularMovies(int page = 1)
    {
        return await tmdbClient.GetPopularMoviesAsync(page);
    }

    public async Task<TmdbMovieDetails?> GetMovieDetails(int id = 157336)
    {
        return await tmdbClient.GetMovieDetailsAsync(id);
    }
}
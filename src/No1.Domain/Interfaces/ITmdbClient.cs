using No1.Models;
using System.Threading.Tasks;

namespace No1.Interfaces;

public interface ITmdbClient
{
    Task<TmdbPopularMoviePagedResponse?> GetPopularMoviesAsync(int page);

    Task<TmdbMovieDetails?> GetMovieDetailsAsync(int id);
}
using Microsoft.Extensions.Options;
using No1.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace No1.Tmdb;

public class TmdbAuthenticationHandler(IOptions<TmdbOptions> tmdbOptions) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tmdbOptions.Value.AccessTokenAuth);

        return await base.SendAsync(request, cancellationToken);
    }
}
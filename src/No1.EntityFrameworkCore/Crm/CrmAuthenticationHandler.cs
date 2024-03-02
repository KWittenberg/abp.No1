using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using No1.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace No1.Crm;

public class CrmAuthenticationHandler(IOptions<CrmOptions> crmOptions) : DelegatingHandler
{
    private readonly CrmOptions _crmOptions = crmOptions.Value;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var scopes = new[] { $"{_crmOptions.BaseUrl}/.default" };
        var app = ConfidentialClientApplicationBuilder.Create(_crmOptions.ClientId).WithClientSecret(_crmOptions.ClientSecret).WithAuthority(_crmOptions.AccessTokenUrl).Build();
        var result = app.AcquireTokenForClient(scopes).ExecuteAsync(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.Result.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
using Microsoft.Extensions.Options;
using No1.Exceptions;
using No1.Interfaces;
using No1.Models;
using No1.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace No1.Crm;

public class CrmClient(HttpClient client, IOptions<CrmOptions> crmOptions) : ICrmClient, ITransientDependency
{
    private readonly CrmOptions _crmOptions = crmOptions.Value;


    public async Task<AdvisorModel?> GetAdvisorById(string id)
    {
        var response = await client.GetAsync($"{_crmOptions.BaseUrl}/{_crmOptions.AdvisorUrl.Replace("{id}", id)}");
        if (!response.IsSuccessStatusCode) throw new InvalidResponseException(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AdvisorModel>(content);
    }

    public async Task<DossierSubmittalModel?> GetDossierSubmittalId(string id)
    {
        var response = await client.GetAsync($"{_crmOptions.BaseUrl}/{_crmOptions.DossierUrl.Replace("{id}", id)}");
        if (!response.IsSuccessStatusCode) throw new InvalidResponseException(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<DossierSubmittalModel>(content);
    }

    public async Task<VacancyModel?> GetVacancyById(string id)
    {
        var response = await client.GetAsync($"{_crmOptions.BaseUrl}/{_crmOptions.VacancyUrl.Replace("{id}", id)}");
        if (!response.IsSuccessStatusCode) throw new InvalidResponseException(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<VacancyModel>(content);
    }
}
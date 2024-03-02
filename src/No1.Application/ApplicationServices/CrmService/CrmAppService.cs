using No1.Interfaces;
using No1.Models;
using System.Threading.Tasks;

namespace No1.ApplicationServices.CrmService;

public class CrmAppService(ICrmClient crmClient, IEncryption encryption) : No1AppService
{
    public string Decrypt(string input = "TzIyMjgyNjQ3")
    {
        return encryption.Decrypt(input);
    }

    public string Encrypt(string input = "M00060425")
    {
        return encryption.Encrypt(input);
    }


    public async Task<AdvisorModel?> GetAdvisorById(string id = "P046579")
    {
        return await crmClient.GetAdvisorById(id);
    }
    
    public async Task<DossierSubmittalModel?> GetDossierSubmittalId(string id = "DS0235765")
    {
        return await crmClient.GetDossierSubmittalId(id);
    }

    public async Task<VacancyModel?> GetVacancyById(string id = "M00060425")
    {
        return await crmClient.GetVacancyById(id);
    }
}
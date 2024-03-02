using No1.Models;
using System.Threading.Tasks;

namespace No1.Interfaces;

public interface ICrmClient
{
    Task<AdvisorModel?> GetAdvisorById(string id);

    Task<DossierSubmittalModel?> GetDossierSubmittalId(string id);

    Task<VacancyModel?> GetVacancyById(string id);
}
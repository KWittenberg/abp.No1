namespace No1.Options;

public class CrmOptions 
{
    public const string Section = "Crm";

    public string AccessTokenUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }



    public string BaseUrl { get; set; }
    public string AdvisorUrl { get; set; }
    public string DossierUrl { get; set; }
    public string VacancyUrl { get; set; }
}
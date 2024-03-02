using System;

namespace No1.ApplicationServices.CountryService.CreateCity;

public class CreateCityInput
{
    public Guid CountryId { get; set; }

    public string Name { get; set; }
    
    public string PostCode { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
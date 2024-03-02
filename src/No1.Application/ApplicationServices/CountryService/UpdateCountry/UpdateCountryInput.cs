using System;
using No1.ApplicationServices.CountryService.CreateCountry;

namespace No1.ApplicationServices.CountryService.UpdateCountry;

public class UpdateCountryInput : CreateCountryInput
{
    public Guid CountryId { get; set; }
}
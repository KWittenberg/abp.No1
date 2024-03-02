using System;
using No1.ApplicationServices.CountryService.CreateCity;

namespace No1.ApplicationServices.CountryService.UpdateCity;

public class UpdateCityInput : CreateCityInput
{
    public Guid CityId { get; set; }
}
using No1.Entities.CountryAggregate;
using System.Collections.Generic;

namespace No1.Seeds.Models;

public class SeedCountriesAndCities
{
    public string Name { get; set; }

    public List<CountryTranslation> Translations { get; set; }

    public List<SeedCity> Cities { get; set; }
}
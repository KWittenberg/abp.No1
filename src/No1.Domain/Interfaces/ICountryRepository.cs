using No1.Entities.CountryAggregate;
using No1.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace No1.Interfaces;

public interface ICountryRepository : ICrudRepository<Country>
{
    //Task<Country> GetCountryAsync(Guid countryId);

    Task<IList<City>> GetCitiesAsync(CancellationToken cancellationToken = default);

    Task<City?> GetCityAsync(Guid cityId, CancellationToken cancellationToken = default);

    Task<IList<CountryTranslation>> GetCountryTranslationsAsync(CancellationToken cancellationToken = default);
}
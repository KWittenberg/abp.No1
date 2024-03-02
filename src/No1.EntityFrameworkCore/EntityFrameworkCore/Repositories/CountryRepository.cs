using Microsoft.EntityFrameworkCore;
using No1.Entities.CountryAggregate;
using No1.EntityFrameworkCore.Repositories.Common;
using No1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace No1.EntityFrameworkCore.Repositories;

public class CountryRepository : CrudRepository<Country>, ICountryRepository, ITransientDependency
{
    private readonly IDbContextProvider<No1DbContext> _dbContextProvider;

    public CountryRepository(IDbContextProvider<No1DbContext> dbContextProvider) : base(dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    protected override IQueryable<Country> WithDetails(IQueryable<Country> query)
    {
        return query.Include(x => x.Cities).Include(x => x.Translations);
    }

    //public async Task<Country> GetCountryAsync(Guid countryId)
    //{
    //    var context = await _dbContextProvider.GetDbContextAsync();
    //    var country = await context.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Id == countryId);

    //    return country;
    //}
    
    public async Task<IList<City>> GetCitiesAsync(CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        var cities = await context.Cities.AsNoTracking().ToListAsync(cancellationToken);

        return cities;
    }

    public async Task<City?> GetCityAsync(Guid cityId, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        var city = await context.Cities.AsNoTracking().FirstOrDefaultAsync(city => city.Id == cityId, cancellationToken);

        return city;
    }

    public async Task<IList<CountryTranslation>> GetCountryTranslationsAsync(CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        var countryTranslations = await context.CountryTranslations.AsNoTracking().ToListAsync(cancellationToken);

        return countryTranslations;
    }
}
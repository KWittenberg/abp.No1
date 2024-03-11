using No1.ApplicationServices.CountryService.CreateCity;
using No1.ApplicationServices.CountryService.CreateCountry;
using No1.ApplicationServices.CountryService.UpdateCity;
using No1.ApplicationServices.CountryService.UpdateCountry;
using No1.Entities.CountryAggregate;
using No1.Interfaces;
using No1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace No1.ApplicationServices.CountryService;

public class CountryAppService(ICountryRepository countryRepository) : No1AppService
{
    public async Task<IList<CountryOutput>> GetCountries()
    {
        var countries = await countryRepository.GetAllAsync();

        return ObjectMapper.Map<IList<Country>, IList<CountryOutput>>(countries);
    }

    public async Task<CountryOutput> GetCountry(Guid id)
    {
        var country = await countryRepository.GetAsync(country => country.Id == id);

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), id);
        }

        return ObjectMapper.Map<Country, CountryOutput>(country);
    }

    public virtual async Task<CountryOutput> CreateCountry(CreateCountryInput input)
    {
        var country = new Country(GuidGenerator.Create(), input.Name);

        await countryRepository.InsertAsync(country);

        return ObjectMapper.Map<Country, CountryOutput>(country);
    }

    public virtual async Task<CountryOutput> UpdateCountry(UpdateCountryInput input)
    {
        var country = await countryRepository.GetAsync(country => country.Id == input.CountryId);

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), input.CountryId);
        }

        country.SetName(input.Name);

        await countryRepository.UpdateAsync(country);

        return ObjectMapper.Map<Country, CountryOutput>(country);
    }

    public async Task DeleteCountry(Guid id)
    {
        var country = await countryRepository.GetAsync(country => country.Id == id);

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), id);
        }

        await countryRepository.DeleteAsync(country);
    }


    public async Task<IList<CityOutput>> GetCities()
    {
        var cities = await countryRepository.GetCitiesAsync();

        return ObjectMapper.Map<IList<City>, IList<CityOutput>>(cities);
    }

    public async Task<CityOutput> GetCity(Guid id)
    {
        var country = await countryRepository.GetAsync(c => c.Cities.Any(city => city.Id == id));

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), id);
        }

        var city = country.Cities.FirstOrDefault(city => city.Id == id);

        if (city is null)
        {
            throw new EntityNotFoundException(typeof(City), id);
        }

        return ObjectMapper.Map<City, CityOutput>(city);
    }

    public async Task<CityOutput> GetCityByName(string cityName)
    {
        var country = await countryRepository.GetAsync(c => c.Cities.Any(city => city.Name == cityName));

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), cityName);
        }

        var city = country.Cities.FirstOrDefault(city => city.Name == cityName);

        if (city is null)
        {
            throw new EntityNotFoundException(typeof(City), cityName);
        }

        return ObjectMapper.Map<City, CityOutput>(city);
    }

    public virtual async Task<CountryOutput> CreateCity(CreateCityInput input)
    {
        var country = await countryRepository.GetAsync(country => country.Id == input.CountryId);

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), input.CountryId);
        }

        country.AddCity(GuidGenerator.Create(), input.Name, input.PostCode, input.Longitude, input.Latitude);

        await countryRepository.UpdateAsync(country);

        return ObjectMapper.Map<Country, CountryOutput>(country);
    }

    public virtual async Task<CountryOutput> UpdateCity(UpdateCityInput input)
    {
        var country = await countryRepository.GetAsync(country => country.Id == input.CountryId);

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), input.CountryId);
        }

        country.UpdateCity(input.CityId, input.Name);

        await countryRepository.UpdateAsync(country);

        return ObjectMapper.Map<Country, CountryOutput>(country);
    }

    public async Task<CountryOutput> DeleteCity(Guid cityId)
    {
        var country = await countryRepository
            .GetAsync(country => country.Cities.Any(city => city.Id == cityId));

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), cityId);
        }

        country.RemoveCity(cityId);

        await countryRepository.UpdateAsync(country);

        return ObjectMapper.Map<Country, CountryOutput>(country);
    }
}
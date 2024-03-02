using No1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using No1.MultiLingualObjects;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace No1.Entities.CountryAggregate;

public class Country : AuditedAggregateRoot<Guid>, IMultiLingualObject<CountryTranslation>
{
    protected Country()
    {
    }

    public Country(Guid id, string name) : base(id)
    {
        SetName(name);

        Cities = new List<City>();
        Translations = new List<CountryTranslation>();
    }

    public string Name { get; private set; }

    public IList<City> Cities { get; }

    public IList<CountryTranslation> Translations { get; set; }

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CountryConsts.NameLength);
    }

    public void AddCity(Guid cityId, string name, string postCode, double longitude, double latitude)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (Cities.Any(x => x.Name.ToLower() == name.ToLower()))
        {
            throw new EntityAlreadyExistsException(typeof(City), name);
        }

        Cities.Add(new City(cityId, name, postCode, longitude, latitude));
    }

    public void UpdateCity(Guid cityId, string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var city = Cities.FirstOrDefault(x => x.Id == cityId);

        if (city is null)
        {
            throw new EntityNotFoundException(typeof(City));
        }

        if (Cities.Any(x => x.Id != city.Id && x.Name.ToLower() == name.ToLower()))
        {
            throw new EntityAlreadyExistsException(typeof(City), name);
        }

        city.SetName(name);
    }

    public void RemoveCity(Guid cityId)
    {
        var city = Cities.FirstOrDefault(x => x.Id == cityId);

        if (city is null)
        {
            throw new EntityNotFoundException(typeof(City));
        }

        Cities.Remove(city);
    }
}
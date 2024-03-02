using NetTopologySuite.Geometries;
using No1.Exceptions;
using No1.MultiLingualObjects;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace No1.Entities.CountryAggregate;

public class City : AuditedEntity<Guid>, IMultiLingualObject<CityTranslation>
{
    private const int Wgs84Srid = 4326;

    protected City()
    {
    }

    public City(Guid id, string name, string postCode, double longitude, double latitude) : base(id)
    {
        SetName(name);
        SetPostCode(postCode);
        SetLocation(longitude, latitude);
        
        Translations = new List<CityTranslation>();
    }

    public Guid CountryId { get; private set; }

    public string Name { get; private set; }
    
    public string PostCode { get; private set; }

    public Point Location { get; private set; }

    public IList<CityTranslation> Translations { get; set; }

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CityConsts.NameLength);
    }

    public void SetPostCode(string postCode)
    {
        PostCode = Check.NotNullOrWhiteSpace(postCode, nameof(postCode), CityConsts.PostCodeLength);
    }

    public void SetLocation(double longitude, double latitude)
    {
        if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
        {
            throw new InvalidGeoLocationException();
        }

        Location = new Point(longitude, latitude) { SRID = Wgs84Srid };
    }
}
using System;
using No1.MultiLingualObjects;
using Volo.Abp.Domain.Entities;

namespace No1.Entities.CountryAggregate;

public class CityTranslation : Entity, IObjectTranslation
{
    public Guid CityId { get; set; }

    public string Name { get; set; }

    public string Language { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { CityId, Language };
    }
}
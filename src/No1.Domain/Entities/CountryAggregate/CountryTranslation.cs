using No1.MultiLingualObjects;
using System;
using Volo.Abp.Domain.Entities;

namespace No1.Entities.CountryAggregate;

public class CountryTranslation : Entity, IObjectTranslation
{
    public Guid CountryId { get; set; }

    public string Name { get; set; }

    public string Language { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { CountryId, Language };
    }
}
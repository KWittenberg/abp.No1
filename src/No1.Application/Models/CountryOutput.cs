using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace No1.Models;

public class CountryOutput : AuditedEntityDto<Guid>
{
    public string Name { get; set; }


    public IList<CityTranslationOutput> Cities { get; set; }

    public IList<CountryTranslationOutput> Translations { get; set; }
}
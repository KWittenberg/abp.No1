using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace No1.Models;

public class CityOutput : AuditedEntityDto<Guid>
{
    public string Name { get; set; }
    
    public string PostCode { get; set; }
    
    public double Longitude { get; set; }
    
    public double Latitude { get; set; }
    
    public IList<CityTranslationOutput> Translations { get; set; }
}
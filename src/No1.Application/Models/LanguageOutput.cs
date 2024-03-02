using System;
using Volo.Abp.Application.Dtos;

namespace No1.Models;

public class LanguageOutput : AuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public string Locale { get; set; }
}
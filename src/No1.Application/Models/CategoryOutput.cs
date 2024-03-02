using System;
using Volo.Abp.Application.Dtos;

namespace No1.Models;

public class CategoryOutput : AuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }
}
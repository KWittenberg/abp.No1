using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace No1.Models;

public class ApplicationUserOutput : EntityDto<Guid>
{
    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public string Country { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }

    public string AvatarUrl { get; set; }

    public IList<Guid> RoleIds { get; set; }

    public bool IsActive { get; set; }
}
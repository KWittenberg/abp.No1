using System;
using System.Collections.Generic;

namespace No1.ApplicationServices.ApplicationUserService.Update;

public class UpdateApplicationUserInput
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public string PhoneNumber { get; set; }


    public string Country { get; set; }

    public string PostCode { get; set; }

    public string City { get; set; }

    public string Street { get; set; }


    public IList<Guid> RoleIds { get; set; }

    public bool IsActive { get; set; }
}
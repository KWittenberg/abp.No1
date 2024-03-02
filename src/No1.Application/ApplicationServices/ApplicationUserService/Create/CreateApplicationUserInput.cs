using System;
using System.Collections.Generic;

namespace No1.ApplicationServices.ApplicationUserService.Create;

public class CreateApplicationUserInput
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public string Email { get; set; }

    public string PhoneNumber { get; set; }


    public string Country { get; set; }

    public string PostCode { get; set; }

    public string City { get; set; }

    public string Street { get; set; }
    

    public IList<Guid> RoleIds { get; set; }

    public string Password { get; set; }
}
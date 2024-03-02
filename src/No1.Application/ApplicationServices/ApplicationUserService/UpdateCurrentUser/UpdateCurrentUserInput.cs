using System;

namespace No1.ApplicationServices.ApplicationUserService.UpdateCurrentUser;

public class UpdateCurrentUserInput
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public string PhoneNumber { get; set; }

    public string Country { get; set; }
    
    public string PostCode { get; set; }
    
    public string City { get; set; }
    
    public string Street { get; set; }
}
using System;

namespace No1.ApplicationServices.CategoryService.CreateEvent;

public class CreateEventInput
{
    public required string Name { get; set; }

    public DateTime StartEvent { get; set; }

    public DateTime EndEvent { get; set; }
    
    public string? Description { get; set; }
    
    public string? Location { get; set; }
}
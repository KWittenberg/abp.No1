using System;
using System.ComponentModel;

namespace No1.Templates.Email.Common;

public class SendMailModel
{
    [DefaultValue("Summary")]
    public string Summary { get; set; }

    [DefaultValue("Factory X")]
    public string OrganizerName { get; set; }

    [DefaultValue("info@factory-x.hr")]
    public string OrganizerEmail { get; set; }

    [DefaultValue(".NET Developer")]
    public string Role { get; set; }

    [DefaultValue("Krešimir Wittenberg")]
    public string AttendeeName { get; set; }

    [DefaultValue("kresimir.vitenberg@factory-x.hr")]
    public string AttendeeEmail { get; set; }

    [DefaultValue("2023-12-08T08:00:00")]
    public DateTime DateStart { get; set; }

    [DefaultValue("10000 Zagreb, Miramarska 123.")]
    public string Location { get; set; }

    [DefaultValue("Description")]
    public string Description { get; set; }

    [DefaultValue(60)]
    public int Duration { get; set; }

    [DefaultValue(30)]
    public int Reminder { get; set; }

    [DefaultValue("CalendarEvent.ics")]
    public string AttachmentName { get; set; }
}
using No1.ApplicationServices.EmailService.Input;
using No1.Interfaces;
using No1.Models;
using No1.Templates.Email.TestEmail;
using System.Threading.Tasks;

//using Ical.Net;
//using Ical.Net.CalendarComponents;
//using Ical.Net.DataTypes;
//using Ical.Net.Serialization;

namespace No1.ApplicationServices.EmailService;

public class EmailAppService(IEmailManager emailManager) : No1AppService
{
    //private readonly ITemplateRenderer _templateRenderer;
    //private readonly ICalendarEvent _calendarEvent;

    public async Task<SendMailOutput> SendTestEmail(SendEmailInput input)
    {
        await emailManager.SendTestEmail(input.FromEmail, input.ToEmail, input.Subject, input.Body);

        return new SendMailOutput { SendDate = input.SendDate };
    }

    public async Task<SendMailOutput> SendTestEmail_Model(SendEmailInput input)
    {
        var email = new TestEmailModel
        {
            FromEmail = input.FromEmail,
            ToEmail = input.ToEmail,
            Subject = input.Subject,
            Body = input.Body
        };

        await emailManager.SendTestEmail(email);

        return new SendMailOutput { SendDate = input.SendDate };
    }

    public async Task<SendMailOutput> SendTestEmail_Model_WithName(SendEmailInput input)
    {
        var email = new TestEmailModel
        {
            FromName = input.FromName,
            FromEmail = input.FromEmail,
            ToName = input.ToName,
            ToEmail = input.ToEmail,
            Subject = input.Subject,
            Body = input.Body
        };

        await emailManager.SendTestEmailWithName(email);

        return new SendMailOutput { SendDate = input.SendDate };
    }

    public async Task<SendMailOutput> SendTestEmail_Model_WithNameAndTemplate(SendEmailInput input)
    {
        var email = new TestEmailModel
        {
            FromName = input.FromName,
            FromEmail = input.FromEmail,
            ToName = input.ToName,
            ToEmail = input.ToEmail,
            Subject = input.Subject,
            Body = input.Body
        };

        await emailManager.SendTestEmailWithNameAndTemplateV2(email);

        return new SendMailOutput { SendDate = input.SendDate };
    }

    
    //public async Task<SendMailOutput> GenerateYellowSharkEmailWithCalendarEvent(SendMailModel input)
    //{
    //    var bodyHtml = await _templateRenderer.RenderAsync("YellowShark", new YellowSharkModel
    //    {
    //        Summary = input.Summary,
    //        OrganizerName = input.OrganizerName,
    //        OrganizerEmail = input.OrganizerEmail,
    //        Role = input.Role,
    //        AttendeeName = input.AttendeeName,
    //        AttendeeEmail = input.AttendeeEmail,
    //        DateStart = input.DateStart,
    //        Location = input.Location,
    //        Duration = input.Duration,
    //        Description = input.Description
    //    });

    //    var calendarEvent = _calendarEvent.CreateCalendarEvent(
    //        input.Summary,
    //        input.OrganizerName,
    //        input.OrganizerEmail,
    //        input.AttendeeName,
    //        input.AttendeeEmail,
    //        input.DateStart,
    //        input.Location,
    //        input.Description,
    //        input.Duration,
    //        input.Reminder);

    //    await _emailManager.SendEmailWithCalendarEvent(input, bodyHtml, calendarEvent);

    //    //var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(calendarEvent));
    //    //var attachment = new Attachment(memoryStream, input.AttachmentName, "text/calendar");

    //    //var mail = new MailMessage();
    //    //mail.From = new MailAddress(input.OrganizerEmail, input.OrganizerName);
    //    //mail.To.Add(new MailAddress(input.AttendeeEmail, input.AttendeeName));
    //    //mail.Subject = input.Summary;
    //    //mail.IsBodyHtml = true;
    //    //mail.Priority = MailPriority.High;
    //    //mail.Body = bodyHtml;
    //    //mail.Attachments.Add(attachment);

    //    //await _emailSender.SendAsync(mail);

    //    return new SendMailOutput { SendDate = DateTime.UtcNow };
    //}

    //public async Task<SendMailOutput> Send2(SendInput input)
    //{
    //    var testHtml = await _templateRenderer.RenderAsync("TestEmail", new TestEmailModel { Name = "YellowShark" });

    //    await _emailSender.SendAsync("kresimir.vitenberg@factoryxhrvatska.onmicrosoft.com", "Test YellowShark Email", testHtml);

    //    return new SendMailOutput { SendDate = input.SendDate };
    //}

    //public async Task<SendMailOutput> Send(SendInput input)
    //{
    //    var testHtml = await _templateRenderer.RenderAsync("TestEmail", new TestEmailModel { Name = "John" });

    //    await _emailSender.SendAsync("kresimir.vitenberg@factoryxhrvatska.onmicrosoft.com", "Email Test", testHtml);

    //    return new SendMailOutput { SendDate = input.SendDate };
    //}

    //public async Task SendEmailWithCalendarEventOld(SendMailInput input)
    //{
    //    var minutesBeforeEvent = 30;
    //    var dateEnd = input.DateStart.AddMinutes(60);
    //    var summary = $"Vorstellungsgespräch - {input.Role}";
    //    var description = $"Vorstellungsgespräch - {input.Role} bei {input.OrganizerName}";

    //    var sb = new StringBuilder();

    //    //START the Calendar Item
    //    sb.AppendLine("BEGIN:VCALENDAR");
    //    sb.AppendLine("VERSION:2.0");
    //    sb.AppendLine("PRODID:-//YellowShark//");
    //    sb.AppendLine("CALSCALE:GREGORIAN");
    //    sb.AppendLine("METHOD:REQUEST");

    //    //Create a Time zone if needed, TZID to be used in the event itself
    //    sb.AppendLine("BEGIN:VTIMEZONE");
    //    sb.AppendLine("TZID:Europe/Zurich");
    //    sb.AppendLine("BEGIN:STANDARD");
    //    sb.AppendLine("TZOFFSETTO:+0100");
    //    sb.AppendLine("TZOFFSETFROM:+0100");
    //    sb.AppendLine("END:STANDARD");
    //    sb.AppendLine("END:VTIMEZONE");

    //    //Add the Event
    //    sb.AppendLine("BEGIN:VEVENT");
    //    sb.AppendLine($"UID:{Guid.NewGuid()}");
    //    sb.AppendLine($"DTSTAMP:{DateTime.UtcNow.ToString("yyyyMMddTHHmm00")}");
    //    sb.AppendLine("DTSTART:" + input.DateStart.ToString("yyyyMMddTHHmm00"));
    //    sb.AppendLine("DTEND:" + dateEnd.ToString("yyyyMMddTHHmm00"));

    //    sb.AppendLine($"SUMMARY:{summary}");
    //    sb.AppendLine($"ORGANIZER;CN={input.OrganizerName}:mailto:{input.OrganizerEmail}");
    //    sb.AppendLine($"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN={input.AttendeeName}:mailto:{input.AttendeeEmail}");
    //    sb.AppendLine($"LOCATION:{input.Location}");
    //    sb.AppendLine($"DESCRIPTION:{description}");
    //    //sb.AppendLine("PRIORITY:3"); // 1 to 9

    //    sb.AppendLine("BEGIN:VALARM");
    //    sb.AppendLine("ACTION:DISPLAY");
    //    sb.AppendLine("DESCRIPTION:This is an event reminder!");
    //    sb.AppendLine($"TRIGGER:-P0DT0H{minutesBeforeEvent}M0S");
    //    sb.AppendLine("END:VALARM");
    //    sb.AppendLine("END:VEVENT");
    //    sb.AppendLine("END:VCALENDAR");

    //    var calendarItem = sb.ToString();

    //    //var filePath = $"D:/{fileName}.ics";
    //    //await File.WriteAllTextAsync(filePath, calendarItem);

    //    var bodyHtml = await _templateRenderer.RenderAsync("TestEmail", new TestEmailModel { Name = "YellowShark" });

    //    var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(calendarItem));
    //    var attachment = new Attachment(memoryStream, "Vorstellungsgespraech.ics", "text/calendar");

    //    var mail = new MailMessage();
    //    mail.From = new MailAddress(input.OrganizerEmail, input.OrganizerName);
    //    mail.To.Add(new MailAddress(input.AttendeeEmail, input.AttendeeName));
    //    mail.Subject = summary;
    //    mail.IsBodyHtml = true;
    //    mail.Priority = MailPriority.High;
    //    mail.Body = bodyHtml;
    //    mail.Attachments.Add(attachment);

    //    await _emailSender.SendAsync(mail);
    //}

    //private static string CreateCalendarEvent(
    //    string summary,
    //    string organizerName,
    //    string organizerEmail,
    //    string attendeeName,
    //    string attendeeEmail,
    //    DateTime start,
    //    string location,
    //    string description,
    //    int duration = 60,
    //    int reminder = 30)
    //{
    //    var end = start.AddMinutes(duration);

    //    var sb = new StringBuilder();

    //    //START the Calendar Item
    //    sb.AppendLine("BEGIN:VCALENDAR");
    //    sb.AppendLine("VERSION:2.0");
    //    sb.AppendLine("PRODID:-//Factory X//");
    //    sb.AppendLine("CALSCALE:GREGORIAN");
    //    sb.AppendLine("METHOD:REQUEST");

    //    //Create a Time zone if needed, TZID to be used in the event itself
    //    sb.AppendLine("BEGIN:VTIMEZONE");
    //    sb.AppendLine("TZID:Europe/Zurich");
    //    sb.AppendLine("BEGIN:STANDARD");
    //    sb.AppendLine("TZOFFSETTO:+0100");
    //    sb.AppendLine("TZOFFSETFROM:+0100");
    //    sb.AppendLine("END:STANDARD");
    //    sb.AppendLine("END:VTIMEZONE");

    //    //Add the Event
    //    sb.AppendLine("BEGIN:VEVENT");
    //    sb.AppendLine($"UID:{Guid.NewGuid()}");
    //    sb.AppendLine($"DTSTAMP:{DateTime.UtcNow.ToString("yyyyMMddTHHmm00")}");
    //    sb.AppendLine("DTSTART:" + start.ToString("yyyyMMddTHHmm00"));
    //    sb.AppendLine("DTEND:" + end.ToString("yyyyMMddTHHmm00"));

    //    sb.AppendLine($"SUMMARY:{summary}");
    //    sb.AppendLine($"ORGANIZER;CN={organizerName}:mailto:{organizerEmail}");
    //    sb.AppendLine($"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN={attendeeName}:mailto:{attendeeEmail}");
    //    sb.AppendLine($"LOCATION:{location}");
    //    sb.AppendLine($"DESCRIPTION:{description}");
    //    //sb.AppendLine("PRIORITY:3"); // 1 to 9

    //    sb.AppendLine("BEGIN:VALARM");
    //    sb.AppendLine("ACTION:DISPLAY");
    //    sb.AppendLine("DESCRIPTION:This is an event reminder!");
    //    sb.AppendLine($"TRIGGER:-P0DT0H{reminder}M0S");
    //    sb.AppendLine("END:VALARM");
    //    sb.AppendLine("END:VEVENT");
    //    sb.AppendLine("END:VCALENDAR");

    //    return sb.ToString();
    //}




    //public async Task CreateEvent(CreateEventInput input)
    //{
    //    var filePath = $"D:/{input.Name}.ics";

    //    var calendarEvent = new CalendarEvent
    //    {
    //        Start = new CalDateTime(input.StartEvent),
    //        End = new CalDateTime(input.EndEvent),
    //        Location = input.Location,
    //        IsAllDay = true,
    //        Summary = input.Name,
    //        Description = input.Description
    //    };

    //    var lat = 80;
    //    var lng = 100;
    //    calendarEvent.GeographicLocation = new GeographicLocation(lat, lng);


    //    var calendar = new Calendar();
    //    calendar.Events.Add(calendarEvent);

    //    var serializer = new CalendarSerializer();
    //    var serializedCalendar = serializer.SerializeToString(calendar);

    //    await File.WriteAllTextAsync(filePath, serializedCalendar);
    //}

    //public async Task Calendar()
    //{
    //    var filePath = "D:/Vorstellungsgespraech.ics";

    //    var calendar = new Calendar
    //    {
    //        ProductId = "Factory X"
    //    };

    //    var calendarEvent = new CalendarEvent
    //    {
    //        Organizer = new Organizer("mailto:admin@gmail.hr")
    //        {
    //            CommonName = "Printcolor Screen AG"
    //        },

    //        Summary = "Vorstellungsgespräch - [Financial Accountant(80 - 100 %)] mit[Schmitt Sean]",
    //        Description = "",

    //        Start = new CalDateTime(2023, 11, 29, 16, 0, 0),
    //        End = new CalDateTime(2023, 11, 29, 17, 0, 0),
    //        IsAllDay = false,

    //        Location = "adresa unešena od strane firme ili Online"
    //    };

    //    calendar.Method = "REQUEST";  // PUBLISH - adding multiple events to a calendar
    //                                  // REQUEST - creates a calendar invite - you have to include the orginizer (ORGANIZER;CN=Kejo:mailto:kejo@net.hr)
    //    calendar.AddTimeZone(new VTimeZone("Europe/Zurich"));
    //    calendar.Events.Add(calendarEvent);

    //    var calendarSerializer = new CalendarSerializer();
    //    var serializedCalendar = calendarSerializer.SerializeToString(calendar);

    //    await File.WriteAllTextAsync(filePath, serializedCalendar);
    //}

    //public async Task CalendarEvent()
    //{
    //    var dateStart = new DateTime(2023, 12, 01, 8, 0, 0);
    //    var dateEnd = dateStart.AddMinutes(60);
    //    int minutesBeforeEvent = 30;

    //    var roleName = "Financial Accountant";
    //    var summary = $"Vorstellungsgespräch - {roleName}"; // Max 75 char

    //    var organizerName = "Printcolor Screen AG";
    //    var organizerEmail = "printcolor.screen.ag@gmail.com";

    //    var attendeeName = "Krešimir Wittenberg";
    //    var attendeeEmail = "kresimir.wittenberg@gmail.com";


    //    var location = "10000 Zagreb, Miramarska 123.";
    //    var description = $"Vorstellungsgespräch - {roleName} bei {organizerName}";

    //    var fileName = "Vorstellungsgespraech";

    //    var sb = new StringBuilder();

    //    //START the Calendar Item
    //    sb.AppendLine("BEGIN:VCALENDAR");
    //    sb.AppendLine("VERSION:2.0");
    //    sb.AppendLine("PRODID:-//Factory X//");
    //    sb.AppendLine("CALSCALE:GREGORIAN");
    //    sb.AppendLine("METHOD:REQUEST");

    //    //Create a Time zone if needed, TZID to be used in the event itself
    //    sb.AppendLine("BEGIN:VTIMEZONE");
    //    sb.AppendLine("TZID:Europe/Zurich");
    //    sb.AppendLine("BEGIN:STANDARD");
    //    sb.AppendLine("TZOFFSETTO:+0100");
    //    sb.AppendLine("TZOFFSETFROM:+0100");
    //    sb.AppendLine("END:STANDARD");
    //    sb.AppendLine("END:VTIMEZONE");

    //    //Add the Event
    //    sb.AppendLine("BEGIN:VEVENT");
    //    sb.AppendLine($"UID:{Guid.NewGuid()}");
    //    sb.AppendLine($"DTSTAMP:{DateTime.UtcNow.ToString("yyyyMMddTHHmm00")}");
    //    sb.AppendLine("DTSTART:" + dateStart.ToString("yyyyMMddTHHmm00"));
    //    sb.AppendLine("DTEND:" + dateEnd.ToString("yyyyMMddTHHmm00"));

    //    sb.AppendLine($"SUMMARY:{summary}");
    //    sb.AppendLine($"ORGANIZER;CN={organizerName}:mailto:{organizerEmail}");
    //    sb.AppendLine($"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN={attendeeName}:mailto:{attendeeEmail}");
    //    sb.AppendLine($"LOCATION:{location}");
    //    sb.AppendLine($"DESCRIPTION:{description}");
    //    //sb.AppendLine("PRIORITY:3"); // 1 to 9

    //    sb.AppendLine("BEGIN:VALARM");
    //    sb.AppendLine("ACTION:DISPLAY");
    //    sb.AppendLine("DESCRIPTION:This is an event reminder!");
    //    sb.AppendLine($"TRIGGER:-P0DT0H{minutesBeforeEvent}M0S");
    //    sb.AppendLine("END:VALARM");
    //    sb.AppendLine("END:VEVENT");
    //    sb.AppendLine("END:VCALENDAR");

    //    var calendarItem = sb.ToString();

    //    var filePath = $"D:/{fileName}.ics";

    //    await File.WriteAllTextAsync(filePath, calendarItem);
    //}
}
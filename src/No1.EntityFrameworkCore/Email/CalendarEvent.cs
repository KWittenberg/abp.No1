using System;
using System.Text;
using No1.Interfaces;
using Volo.Abp.DependencyInjection;

namespace No1.Email;

public class CalendarEvent: ICalendarEvent, ITransientDependency
{
    public string CreateCalendarEvent(
        string summary,
        string organizerName,
        string organizerEmail,
        string attendeeName,
        string attendeeEmail,
        DateTime start,
        string location,
        string description,
        int duration,
        int reminder)
    {
        var end = start.AddMinutes(duration);

        if (!string.IsNullOrEmpty(organizerEmail))
        {
            organizerEmail = $":mailto:{organizerEmail}";
        }

        if (!string.IsNullOrEmpty(attendeeEmail))
        {
            attendeeEmail = $":mailto:{attendeeEmail}";
        }
        
        var sb = new StringBuilder();

        // START Calendar Item
        sb.AppendLine("BEGIN:VCALENDAR");
        sb.AppendLine("VERSION:2.0");
        sb.AppendLine("PRODID:-//Factory X//");
        sb.AppendLine("CALSCALE:GREGORIAN");
        sb.AppendLine("METHOD:REQUEST");

        // Create a Time zone if needed, TZID to be used in the event itself
        sb.AppendLine("BEGIN:VTIMEZONE");
        sb.AppendLine("TZID:Europe/Zurich");
        sb.AppendLine("BEGIN:STANDARD");
        sb.AppendLine("TZOFFSETTO:+0100");
        sb.AppendLine("TZOFFSETFROM:+0100");
        sb.AppendLine("END:STANDARD");
        sb.AppendLine("END:VTIMEZONE");

        // Add Event
        sb.AppendLine("BEGIN:VEVENT");
        sb.AppendLine($"UID:{Guid.NewGuid()}");
        sb.AppendLine($"DTSTAMP:{DateTime.UtcNow.ToString("yyyyMMddTHHmm00")}");
        sb.AppendLine("DTSTART:" + start.ToString("yyyyMMddTHHmm00"));
        sb.AppendLine("DTEND:" + end.ToString("yyyyMMddTHHmm00"));

        sb.AppendLine($"SUMMARY:{summary}");
        // sb.AppendLine($"ORGANIZER;CN={organizerName}:mailto:{organizerEmail}");
        sb.AppendLine($"ORGANIZER;CN={organizerName}{organizerEmail}");
        // sb.AppendLine($"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=ACCEPTED;RSVP=TRUE;CN={attendeeName}:mailto:{attendeeEmail}");
        sb.AppendLine($"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=ACCEPTED;CN={attendeeName}{attendeeEmail}");
        sb.AppendLine($"LOCATION:{location}");
        sb.AppendLine($"DESCRIPTION:{description}");
        // sb.AppendLine("PRIORITY:3"); // 1 to 9

        // Add Reminder
        sb.AppendLine("BEGIN:VALARM");
        sb.AppendLine("ACTION:DISPLAY");
        sb.AppendLine("DESCRIPTION:This is an event reminder!");
        sb.AppendLine($"TRIGGER:-P0DT0H{reminder}M0S");
        sb.AppendLine("END:VALARM");
        sb.AppendLine("END:VEVENT");
        sb.AppendLine("END:VCALENDAR");

        return sb.ToString();
    }
}
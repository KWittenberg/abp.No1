using System;

namespace No1.Interfaces;

public interface ICalendarEvent
{
    string CreateCalendarEvent(
        string summary,
        string organizerName,
        string organizerEmail,
        string attendeeName,
        string attendeeEmail,
        DateTime start,
        string location,
        string description,
        int duration,
        int reminder);
}
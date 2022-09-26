using outlookCalendarApi.Application.Types;
using System;
using System.Collections.Generic;

namespace outlookCalendarApi.Application.Dtos
{
    public class EventDto
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public BodyDto Body { get; set; }
        public TimeDto Start { get; set; }
        public TimeDto End { get; set; }
        public List<AttendeeDto> Attendees { get; set; }
        public List<LocationDto> Locations { get; set; }
        public ShowAsType ShowAs { get; set; }
        public ImportanceType Importance { get; set; }
        public SensitivityType Sensitivity { get; set; }
        public bool AllowNewTimeProposals { get; set; }
        public bool HideAttendees { get; set; }
        public bool ResponseRequested { get; set; }
        public int ReminderMinutesBeforeStart { get; set; }
        public bool IsOnlineMeeting { get; set; }
        public OnlineMeetingProviderType OnlineMeetingProvider { get; set; }
    }

    public class BodyDto
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class TimeDto
    {
        public string DateTime { get; set; }
        public string TimeZone { get; set; }
    }

    public class EmailDto
    {
        public string Address { get; set; }
        public string Name { get; set; }
    }

    public class AttendeeDto
    {
        public EmailDto EmailAddress { get; set; }
        public AttendeeType Type { get; set; }
    }

    public class LocationDto
    {
        public string DisplayName { get; set; }
        public LocationType LocationType { get; set; }
    }
}

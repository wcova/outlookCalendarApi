using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace outlookCalendarApi.Application.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FieldEventType
    {
        [Display(Name = "Subject")]
        Subject,
        [Display(Name = "Body")]
        Body,
        [Display(Name = "Start")]
        Start,
        [Display(Name = "End")]
        End,
        [Display(Name = "Attendees")]
        Attendees,
        [Display(Name = "Locations")]
        Locations,
        [Display(Name = "ShowAs")]
        ShowAs,
        [Display(Name = "Importance")]
        Importance,
        [Display(Name = "Sensitivity")]
        Sensitivity,
        [Display(Name = "AllowNewTimeProposals")]
        AllowNewTimeProposals,
        [Display(Name = "HideAttendees")]
        HideAttendees,
        [Display(Name = "ResponseRequested")]
        ResponseRequested,
        [Display(Name = "ReminderMinutesBeforeStart")]
        ReminderMinutesBeforeStart,
        [Display(Name = "IsOnlineMeeting")]
        IsOnlineMeeting,
        [Display(Name = "OnlineMeetingProvider")]
        OnlineMeetingProvider
    }
}

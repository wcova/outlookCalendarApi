using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace outlookCalendarApi.Application.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShowAsType
    {
        [Display(Name = "Free")]
        Free, 
        [Display(Name = "Tentative")]
        Tentative,
        [Display(Name = "Busy")]
        Busy,
        [Display(Name = "Oof")]
        Oof,
        [Display(Name = "WorkingElsewhere")]
        WorkingElsewhere,
        [Display(Name = "Unknown")]
        Unknown
    }
}

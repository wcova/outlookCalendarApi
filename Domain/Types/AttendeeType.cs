using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace outlookCalendarApi.Application.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttendeeType
    {
        [Display(Name = "Required")]
        Required,
        [Display(Name = "Optional")]
        Optional,
        [Display(Name = "Resource")]
        Resource
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace outlookCalendarApi.Application.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SensitivityType
    {
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "Personal")]
        Personal,
        [Display(Name = "Private")]
        Private,
        [Display(Name = "Confidential")]
        Confidential
    }
}

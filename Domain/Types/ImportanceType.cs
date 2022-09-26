using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace outlookCalendarApi.Application.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImportanceType
    {
        [Display(Name = "Low")]
        Low,
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "High")]
        High
    }
}

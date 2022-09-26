
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace outlookCalendarApi.Application.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OnlineMeetingProviderType
    {
        [Display(Name = "TeamsForBusiness")]
        TeamsForBusiness,
        [Display(Name = "SkypeForBusiness")]
        SkypeForBusiness,
        [Display(Name = "SkypeForConsumer")]
        SkypeForConsumer,
        [Display(Name = "Unknown")]
        Unknown
    }
}

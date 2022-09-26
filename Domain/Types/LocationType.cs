using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace outlookCalendarApi.Application.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LocationType
    {
        [Display(Name = "Default")]
        Default,
        [Display(Name = "ConferenceRoom")]
        ConferenceRoom,
        [Display(Name = "HomeAddress")]
        HomeAddress,
        [Display(Name = "BusinessAddress")]
        BusinessAddress, 
        [Display(Name = "GeoCoordinates")] 
        GeoCoordinates,
        [Display(Name = "StreetAddress")]
        StreetAddress,
        [Display(Name = "Hotel")]
        Hotel,
        [Display(Name = "Restaurant")]
        Restaurant,
        [Display(Name = "LocalBusiness")]
        LocalBusiness,
        [Display(Name = "PostalAddress")]
        PostalAddress
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;

namespace outlookCalendarApi.Application.Dtos
{
    public class OdataDto<T>
    {
        [JsonProperty(PropertyName = "@odata.count")]
        public int Total { get; set; }
        public List<T> Value { get; set; }
        public Error Error { get; set; }
    }
    public class Error
    {
        public string Code { get; set;}
        public string Message { get; set;}
    }
}

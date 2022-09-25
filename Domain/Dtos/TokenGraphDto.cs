using System.Text.Json.Serialization;

namespace outlookCalendarApi.Domain.Dtos
{
    public class TokenGraphDto
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("Scope")]
        public string Scope { get; set; }

        [JsonPropertyName("expires_in")]
        public int Expires_in { get; set; }

        [JsonPropertyName("ext_expires_in")]
        public int Ext_expires_in { get; set; }

        [JsonPropertyName("access_token")]
        public string Access_token { get; set; }
    }
}

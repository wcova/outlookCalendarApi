namespace outlookCalendarApi.Application.Settings
{
    public class AzureAD
    {
        public string Instance { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string Endpoint_Token { get; set; }
        public string Endpoint_Authorize { get; set; }
    }
}

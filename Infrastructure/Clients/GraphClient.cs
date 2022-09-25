using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using outlookCalendarApi.Domain.Dtos;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace outlookCalendarApi.Infrastructure.Clients
{
    public class GraphClient : ClientBase, IGraphClient
    {
        private AzureADDto _azureAD;

        public GraphClient(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _azureAD = new AzureADDto();
            configuration.GetSection("AzureAd").Bind(_azureAD);
        }

        public async Task<string> GetAccessToken(HttpContext context)
        {
            string accessToken = string.Empty;

            var tokenAD = context.Request.Headers["Authorization"].ToString();
            tokenAD = tokenAD.Replace("Bearer ", string.Empty);

            var body = new
            {
                client_id = _azureAD.ClientId,
                client_secret = _azureAD.ClientSecret,
                scope = _azureAD.Scope,
                requested_token_use = "on_behalf_of",
                assertion = tokenAD,
                grant_type = "urn:ietf:params:oauth:grant-type:jwt-bearer"
            };

            var tokenGraph = await this.PostAsync<TokenGraphDto>(
                _azureAD.Instance + _azureAD.Endpoint_Token,
                body,
                true);

            if (tokenGraph != null)
                accessToken = tokenGraph.Access_token;

            return accessToken;
        }
    }
}
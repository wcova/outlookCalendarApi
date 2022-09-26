using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace outlookCalendarApi.Infrastructure.Clients
{
    public class GraphClient : ClientBase, IGraphClient
    {
        private AzureAD _azureAD;
        private Graph _graph;

        public GraphClient(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _azureAD = new AzureAD();
            configuration.GetSection("AzureAd").Bind(_azureAD);

            _graph = new Graph();
            configuration.GetSection("Graph").Bind(_graph);
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

        public async Task<OdataDto<EventDto>> GetEvents(string tokenGraph, PaggingBase pagging)
        {
            var endpoint = _graph.Instance + _graph.Endpoint_GET_Events;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["$top"] = pagging.PageSize.ToString();
            query["$skip"] = pagging.GetSkip().ToString();
            query["$count"] = "true";

            var events = await this.GetAsync<OdataDto<EventDto>>(
                $"{endpoint}?{query}",
                tokenGraph,
                true);

            return events;
        }

        public async Task<EventDto> GetEventById(string tokenGraph, string id)
        {
            var endpoint = _graph.Instance + _graph.Endpoint_GET_EventById;

            var eventById = await this.GetAsync<EventDto>(
                $"{endpoint}/{id}",
                tokenGraph,
                true);

            return eventById;
        }

        public async Task DeleteEventById(string tokenGraph, string id)
        {
            var endpoint = _graph.Instance + _graph.Endpoint_GET_EventById;

            await this.DeleteAsync(
                $"{endpoint}/{id}",
                tokenGraph,
                true);
        }
    }
}
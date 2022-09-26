using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace outlookCalendarApi.Infrastructure.Clients
{
    public class ClientBase
    {
        private readonly IHttpClientFactory _clientFactory;
        public ClientBase(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> PostAsync<T>(
            string url,
            dynamic body,
            bool IsFormEncoded = false) where T : class
        {
            var client = _clientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(body);
            HttpContent content = null;

            if (IsFormEncoded)
            {
                var bodyToFormEncoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                content = new FormUrlEncodedContent(bodyToFormEncoded);
            }
            else
            {
                content = new StringContent(data, Encoding.UTF8, "application/json");
            }

            var httpResponse = await client.PostAsync(url, content);

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> GetAsync<T>(
            string request,
            string token = "",
            bool IsWithToken = false) where T : class
        {
            var client = _clientFactory.CreateClient();

            if (IsWithToken)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await client.GetAsync(request);

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}

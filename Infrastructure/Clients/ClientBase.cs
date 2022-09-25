using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

            var response =  await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}

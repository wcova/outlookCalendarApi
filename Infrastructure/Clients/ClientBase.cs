//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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
            string token = "",
            bool IsWithToken = false) where T : class
        {
            var client = _clientFactory.CreateClient();
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            HttpContent content = null;

            if (IsWithToken)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var bodyToFormEncoded = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            content = new FormUrlEncodedContent(bodyToFormEncoded);

            var httpResponse = await client.PostAsync(url, content);

            var response = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(response);

                throw new ClientException(
                    string.Format("Code: {0},Message: {1}", error.Code, error.Message),
                    httpResponse.StatusCode);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
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

            if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(response);

                throw new ClientException(
                    string.Format("Code: {0},Message: {1}", error.Code, error.Message),
                    httpResponse.StatusCode);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
        }

        public async Task DeleteAsync(
            string request,
            string token = "",
            bool IsWithToken = false)
        {
            var client = _clientFactory.CreateClient();

            if (IsWithToken)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await client.DeleteAsync(request);

            var response = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                var error = Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(response);

                throw new ClientException(
                    string.Format("Code: {0},Message: {1}", error.Code, error.Message),
                    httpResponse.StatusCode);
            }
        }

        public async Task<T> PostAsync<T>(
            string url,
            T body,
            string token = "",
            bool IsWithToken = false) where T : class
        {
            var client = _clientFactory.CreateClient();
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            HttpContent content = null;

            if (IsWithToken)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(url, content);

            var response = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.StatusCode != System.Net.HttpStatusCode.Created)
            {
                var error = Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(response);

                throw new ClientException(
                    string.Format("Code: {0},Message: {1}", error.Code, error.Message),
                    httpResponse.StatusCode);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> UpdateAsync<T>(
            string url,
            T body,
            string token = "",
            bool IsWithToken = false) where T : class
        {
            var client = _clientFactory.CreateClient();
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            HttpContent content = null;

            if (IsWithToken)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var httpResponse = await client.PatchAsync(url, content);

            var response = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.StatusCode != System.Net.HttpStatusCode.Created)
            {
                var error = Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(response);

                throw new ClientException(
                    string.Format("Code: {0},Message: {1}", error.Code, error.Message),
                    httpResponse.StatusCode);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
        }
    }
}

using DevExtremeAspNetCoreApp1.ApiServices.Interfaces;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace DevExtremeAspNetCoreApp1.ApiServices.Implementations
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T> GetDataAsync<T>(string endPoint, string token = null)
        {
            var baseAddress = "https://localhost:7117/api";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json" }
                }
            };
            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response =
              JsonSerializer.Deserialize<T>(jsonResponse,
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PostDataAsync<T>(string endPoint, string jsonData, string token = null)
        {
            var baseAddress = "https://localhost:7117/api";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json" }
                },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };
            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response =
              JsonSerializer.Deserialize<T>(jsonResponse,
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;

        }

        public async Task<T> DeleteDataAsync<T>(string endPoint, string token = null)
        {
            var baseAddress = "https://localhost:7117/api";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
            {
          {HeaderNames.Accept , "application/json"}
            }
            };

            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response =
              JsonSerializer.Deserialize<T>(jsonResponse,
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PutDataAsync<T>(string endPoint, string jsonData, string token = null)
        {
            var baseAddress = "https://localhost:7117/api";
            var client = _httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json" }
                },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };
            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response =
              JsonSerializer.Deserialize<T>(jsonResponse,
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PatchData<T>(string endPoint, string jsonData, string token = null)
        {
            var baseAddress = "https://localhost:7117/api";
            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri($"{baseAddress}{endPoint}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json" }
                },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };
            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response =
              JsonSerializer.Deserialize<T>(jsonResponse,
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }


    }
}

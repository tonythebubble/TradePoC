using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public class AsyncRepository : IAsyncRepository
    {
        protected readonly HttpClient _httpClient = new HttpClient();
        protected readonly string _baseUrl;

        public AsyncRepository(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        protected async Task<T> PutAsync<T>(string url, T content)
        {
            var response = await _httpClient.PutAsync(_baseUrl + url, CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        protected async Task<T> PostAsync<T>(string url, T content)
        {
            var response = await _httpClient.PostAsync(_baseUrl + url, CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }


    }
}

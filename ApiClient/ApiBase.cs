using ApiClient.Interfaces;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ApiBase : IApiBase
    {
        private Uri _UrlBase = new("https://localhost:5001/api/");
        //private Uri _UrlBase = new("http://felipejsousa-001-site1.itempurl.com/api/");

        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;

        private readonly HttpClient _httpClient = NewHttpClient();

        public async Task<ReturnModel<T>> GetAsync<T>(Uri requestUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(requestUrl);

                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ReturnModel<T>>(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ReturnModel<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 obj = default, HttpContent content = null)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), content ?? JsonHttpContent(obj));

            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReturnModel<T1>>(data);
        }

        public async Task<ReturnModel<T1>> PutAsync<T1, T2>(Uri requestUrl, T2 obj = default, HttpContent content = null)
        {
            var response = await _httpClient.PutAsync(requestUrl.ToString(), content ?? JsonHttpContent<T2>(obj));
            
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReturnModel<T1>>(data);
        }

        public async Task<ReturnModel<T>> DeleteAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.DeleteAsync(requestUrl.ToString());
            
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReturnModel<T>>(data);
        }

        public Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(_UrlBase, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        public HttpContent JsonHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, IgnoreNullSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static HttpClient NewHttpClient()
        {
            HttpClient client = new ();
            if (!string.IsNullOrWhiteSpace(_httpContext?.Session?.GetString("token")?.ToString()))
            {
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _httpContext?.Session?.GetString("token").ToString());
            }
            return client;
        }

        public static JsonSerializerSettings IgnoreNullSettings => new() { NullValueHandling = NullValueHandling.Ignore };

    }
}

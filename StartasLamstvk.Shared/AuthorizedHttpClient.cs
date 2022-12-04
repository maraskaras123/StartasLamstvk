using System.Net;
using StartasLamstvk.Shared.Models.User;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace StartasLamstvk.Shared
{
    public class AuthorizedHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly IJSRuntime _jsRuntime;

        public AuthorizedHttpClient(IHttpClientFactory clientFactory, IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _httpClient = clientFactory.CreateClient("HttpClient");
            _apiUrl = _httpClient.BaseAddress?.ToString();

            var token = _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token").Result;
            if (token is not null && token != string.Empty)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
            }
        }

        public async Task Authorize(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new ("Bearer", token);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", token);
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "token");
        }

        public async Task<List<UserReadModel>> GetUsers()
        {
            var response = await _httpClient.GetAsync(GetUrl(Routes.Users.Endpoint));
            if (await Validate(response))
            {
                return await response.Content.ReadFromJsonAsync<List<UserReadModel>>();
            }

            return new();
        }

        private async Task<bool> Validate(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "token");
            }

            return false;
        }

        private string GetUrl(string requestUrl)
        {
            var path = $"{_apiUrl}{requestUrl}";

            var requestUrlStartPath = path.StartsWith("https://")
                ? "https://"
                : path.StartsWith("http://")
                    ? "http://"
                    : string.Empty;

            if (requestUrlStartPath == string.Empty)
            {
                return requestUrl;
            }

            var relativeEndpointPath = path.Split(requestUrlStartPath)[1];
            var trimmedPath = relativeEndpointPath.Replace("//", "/");
            return $"{requestUrlStartPath}{trimmedPath}";
        }
    }
}
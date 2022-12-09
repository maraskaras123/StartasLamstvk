using StartasLamstvk.Shared.Helpers;
using StartasLamstvk.Shared.Models.Auth;
using System.Net.Http.Json;

namespace StartasLamstvk.Shared.Clients
{
    public class AuthorizedHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthStateContainer _authState;
        public readonly string ApiUrl;

        public AuthorizedHttpClient(IHttpClientFactory clientFactory, AuthStateContainer authState)
        {
            _authState = authState;
            _httpClient = clientFactory.CreateClient("HttpClient");
            ApiUrl = _httpClient.BaseAddress?.ToString();
        }

        public async Task<bool> Login(LoginModel model)
        {
            var response = await PostAsync(Routes.Auth.Login.Endpoint.GetUrl(ApiUrl), model);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<AuthResponse>();
                await _authState.SaveToken(token);
                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            await _httpClient.DeleteAsync(Routes.Auth.Logout.Endpoint.GetUrl(ApiUrl));
            await _authState.Clear();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T model)
        {
            return await SendAsync(HttpMethod.Post, url, model);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await SendAsync(HttpMethod.Get, url);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await SendAsync(HttpMethod.Delete, url);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T model)
        {
            return await SendAsync(HttpMethod.Put, url, model);
        }

        private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, object model = null)
        {
            var request = new HttpRequestMessage(method, url);
            if (model is not null)
            {
                request.Content = JsonContent.Create(model);
            }

            if (_authState.ExpiredAt.HasValue && _authState.ExpiredAt > DateTime.Now)
            {
                request.Headers.Authorization = new ("Bearer", _authState.Token);
            }

            var response = await _httpClient.SendAsync(request);
            return response;
        }
    }
}
using StartasLamstvk.Shared.Helpers;
using StartasLamstvk.Shared.Models.User;
using System.Net.Http.Json;

namespace StartasLamstvk.Shared.Clients
{
    public class UsersHttpClient
    {
        private readonly AuthorizedHttpClient _httpClient;

        public UsersHttpClient(AuthorizedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserReadModel>> GetUsers()
        {
            var response = await _httpClient.GetAsync(Routes.Users.Endpoint.GetUrl(_httpClient.ApiUrl));
            (var success, var unauthorized) = response.Validate();
            if (unauthorized)
            {
                await _httpClient.Logout();
            }

            return success ? await response.Content.ReadFromJsonAsync<List<UserReadModel>>() : new();
        }

        public async Task<UserBaseModel> GetCurrentUser(int userId)
        {
            var url = Routes.Users.UserId.Endpoint.GetUrl(_httpClient.ApiUrl).Replace(Parameters.UserId, userId.ToString());
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<UserBaseModel>();
        }
    }
}

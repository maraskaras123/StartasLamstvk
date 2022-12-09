using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Clients;
using StartasLamstvk.Shared.Helpers;

namespace StartasLamstvk.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var configuration = builder.Configuration;

            builder.Services.AddSingleton<ILocalStorage, LocalStorage>();

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClient("HttpClient", client => client.BaseAddress = new (configuration["ApiUrl"]));
            builder.Services.AddSingleton<AuthStateContainer>();
            builder.Services.AddSingleton<AuthorizedHttpClient>();
            builder.Services.AddSingleton<UsersHttpClient>();

            var host = builder.Build();

            var authState = host.Services.GetRequiredService<AuthStateContainer>();
            var usersHttpClient = host.Services.GetRequiredService<UsersHttpClient>();
            authState.OnChange += async () =>
            {
                if ((authState.ExpiredAt ?? DateTime.MinValue) > DateTime.UtcNow && authState.User is null)
                {
                    authState.User = await usersHttpClient.GetCurrentUser(authState.UserId.Value);
                }
            };

            await host.RunAsync();
        }
    }
}
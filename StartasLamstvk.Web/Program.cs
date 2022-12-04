using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StartasLamstvk.Shared;

namespace StartasLamstvk.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var configuration = builder.Configuration;

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new(configuration["ApiUrl"]) });
            builder.Services.AddHttpClient("HttpClient", client => client.BaseAddress = new (configuration["ApiUrl"]));
            builder.Services.AddScoped<AuthorizedHttpClient>();

            await builder.Build().RunAsync();
        }
    }
}
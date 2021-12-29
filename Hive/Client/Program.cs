using Fluxor;
using Hive.Client.Services;
using Hive.Client.Services.Interfaces;
using Hive.Client.Services.Organizations;
using Hive.Client.Shared.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hive.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthStateProvider>();
            builder.Services.AddSingleton<ActiveNavigationItem>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.ShowTransitionDuration = 200;
                config.SnackbarConfiguration.HideTransitionDuration = 200;
            });

            builder.Services.AddFluxor(options =>
            {
                options.ScanAssemblies(typeof(Program).Assembly);
                options.UseReduxDevTools();
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();

            await builder.Build().RunAsync();
        }
    }
}

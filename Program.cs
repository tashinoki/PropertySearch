using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PropertySearch;
using PropertySearch.UseCases;
using PropertySearch.Infrastructure;
using MatBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IStationUseCase, IStationUseCase>();
builder.Services.AddSingleton<IStationApiClient, StationApiClient>();

builder.Services.AddMatBlazor();
builder.Services.AddMatToaster();

await builder.Build().RunAsync();

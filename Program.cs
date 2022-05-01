using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PropertySearch;
using PropertySearch.Infrastructure;
using PropertySearch.Services;
using PropertySearch.UseCases;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IStationApiClient, StationApiClient>();
builder.Services.AddSingleton<ITrainTransitService, TrainTransitService>();
builder.Services.AddSingleton<IStationUseCase, StationUseCase>();

builder.Services.AddMatBlazor();
builder.Services.AddMatToaster();

await builder.Build().RunAsync();

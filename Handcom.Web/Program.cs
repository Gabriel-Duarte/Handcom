using Blazored.LocalStorage;
using Handcom.Web;
using Handcom.Web.Configuration;
using Handcom.Web.Services.Authentication;
using Handcom.Web.Services.Interface;
using Handcom.Web.Services.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddInfrastructure(builder.Configuration);
var apiHandcom = builder.Configuration["ApiHandcom"];
builder.Services.AddHttpClient("ApiHandcom", options =>
{
    options.BaseAddress = new Uri(apiHandcom);
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();

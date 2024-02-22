using Handcom.Web;
using Handcom.Web.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


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

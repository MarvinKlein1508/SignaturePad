using Demos.Core.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<SignatureInMemoryService>();
await builder.Build().RunAsync();

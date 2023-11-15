using System;
using System.Globalization;
using System.Net.Http;
using AutoMapper;
using AutoMapper.Internal;
using Delivery.Common.Extensions;
using Delivery.Web.App;
using Delivery.Web.BL.Extensions;
using Delivery.Web.BL.Installers;
using Delivery.Web.BL.Options;
using Delivery.Web.DAL.Installers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");

builder.Services.AddInstaller<WebDALInstaller>();
builder.Services.AddInstaller<WebBLInstaller>(apiBaseUrl);
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAutoMapper(configuration =>
{
    configuration.Internal().MethodMappingEnabled = false;
}, typeof(WebBLInstaller));

builder.Services.Configure<LocalDbOptions>(options =>
{
    options.IsLocalDbEnabled = bool.Parse(builder.Configuration.GetSection(nameof(LocalDbOptions))[nameof(LocalDbOptions.IsLocalDbEnabled)]);
});

var host = builder.Build();

var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();

await host.RunAsync();

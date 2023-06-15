using laba555.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<PluginsConfiguration>();
builder.Services.AddSingleton(provider =>
{
    var grayPlugins = new List<Plugin>
    {
        provider.GetService<GrayPlugin>()
    };
    return grayPlugins;
});

builder.Services.AddSingleton(provider =>
{
    var contrastPlugins = new List<Plugin>
    {
        provider.GetService<ContrastPlugin>()
    };
    return contrastPlugins;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

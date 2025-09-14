using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddTransient<IFileRepository>(serviceProvider => new JsonFileRepository(@"sökväg"));
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUIService, UIService>();

using var app = builder.Build();

var menuService = app.Services.GetRequiredService<IMenuService>();
menuService.Start();
//ändrat
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddSingleton<IFileRepository>(serviceProvider => new JsonFileRepository(@"sökväg"));
builder.Services.AddSingleton<MenuService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IUIService, UIService>();
builder.Services.AddSingleton<IProductManager, ProductManager>();

using var app = builder.Build();

var menuService = app.Services.GetRequiredService<MenuService>();

menuService.Start();

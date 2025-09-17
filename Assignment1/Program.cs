using Assignment1;
using Infrastructure.Interfaces;
using Infrastructure.Managers;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var builder = Host.CreateApplicationBuilder();

string filePath = @"c:\data\products.json";
builder.Services.AddSingleton<IFileRepository>(serviceProvider => new JsonFileRepository(filePath, new JsonSerializerOptions { WriteIndented = true }));
builder.Services.AddSingleton<MenuService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IUIService, UIService>();
builder.Services.AddSingleton<IProductManager, ProductManager>();

using var app = builder.Build();

var menuService = app.Services.GetRequiredService<MenuService>();

menuService.Start();

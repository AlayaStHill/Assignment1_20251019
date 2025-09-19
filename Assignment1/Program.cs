using Assignment1;
using Infrastructure.Interfaces;
using Infrastructure.Managers;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var builder = Host.CreateApplicationBuilder();

string filePath = @"c:\data\products.json";

builder.Services.AddSingleton<IFileService>(serviceProvider => new FileService(
    serviceProvider.GetRequiredService<IFileRepository>(),filePath, new JsonSerializerOptions { WriteIndented = true }));
builder.Services.AddSingleton<IFileRepository, JsonFileRepository>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IProductManager, ProductManager>();
builder.Services.AddSingleton<MenuService>();
builder.Services.AddSingleton<IUIService, UIService>();

using var app = builder.Build();

var menuService = app.Services.GetRequiredService<MenuService>();

menuService.Start();

// Lambda: måste man själv ange ALLA beroenden klassen har även om de är registrerade i containern. DI kör lamban för klassen och lambdan skapar instansen. sp.GetRequiredService --> hämtar från DI.



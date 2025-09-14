using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;

IUIService uIService = new UIService();
IFileRepository fileRepository = new JsonFileRepository("sökväg");
IProductService iProductService = new ProductService(fileRepository);

IMenuService menuService = new MenuService(uIService, iProductService);


menuService.DisplayMainMenu();


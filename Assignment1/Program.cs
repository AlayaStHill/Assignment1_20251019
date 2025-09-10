using Infrastructure.Services;

IUIService uIService = new UIService();
IProductService iProductService = new ProductService();
IMenuService menuService = new MenuService(uIService, iProductService);


menuService.DisplayMainMenu();
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class MenuService(IUIService uIService, IProductService productService) 
{
    private readonly IUIService _uIService = uIService;
    private readonly IProductService _productService = productService;

    private bool _isApplicationRunning = true;

    public void Start()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        do
        {
            _uIService.NewPage("=== Välkommen till Produkthanteraren ===");
            List<string> menuOptions = ["Lägg till ny produkt", "Visa produktlista", "Avsluta"];
            _uIService.ShowList(menuOptions);

            int selectedOption = _uIService.GetNumberInput("Välj ett alternativ: ", min: 1, max: menuOptions.Count);

            switch (selectedOption)
            {
                case 1:
                    DisplayAddNewProduct();
                    break;
                case 2:
                    DisplayProductList();
                    break;
                case 3:
                    _isApplicationRunning = false;
                    break;
                default:
                    _uIService.PrintErrorMessage("Ogiltigt val, försök igen...");
                    MainMenu();
                    break;

            }

        } while (_isApplicationRunning);

    }

    private void DisplayProductList()
    {
        _uIService.NewPage("=== Visa produktlista ===");

        IEnumerable<Product> productList = _productService.GetProductList();

        foreach (Product product in productList)
        {
            _uIService.PrintMessage($"Id: {product.Id} - Namn: {product.Name} - Pris: {product.Price} kr");
        }

        if (productList.Count() == 0)
        {
            _uIService.PrintErrorMessage("Listan är tom");
        }
        else
        {
            _uIService.AddSpacing();
        }
        _uIService.PrintMessage("Tryck på varfri tangent för att återgå till menyn...");
        _uIService.WaitForUserRespons();

    }

    private void DisplayAddNewProduct()
    {
        _uIService.NewPage("=== Lägg till ny produkt ===");


        Product newProduct = new Product
        {
            Name = _uIService.UserInput("Ange namn: "),
            Price = _uIService.GetNumberInput("Ange pris: ", min: 1)
        };

        bool success = _productService.AddToProductList(newProduct);

        _uIService.AddSpacing();
        _uIService.PrintMessage($"Produkten {newProduct.Name} lades till.\nTryck på varfri tangent för att återgå till menyn...");
        _uIService.WaitForUserRespons();
    }
}

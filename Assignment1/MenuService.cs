using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Managers;

namespace Assignment1;

public class MenuService(IUIService uIService, IProductManager productManager)
{
    private readonly IUIService _uIService = uIService;
    private readonly IProductManager _productManager = productManager;

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
            List<string> menuOptions = ["Lägg till ny produkt", "Visa en specifik produkt", "Visa produktlista", "Avsluta"];
            _uIService.ShowList(menuOptions);

            int selectedOption = _uIService.GetNumberInput("Välj ett alternativ: ", min: 1, max: menuOptions.Count);

            switch (selectedOption)
            {
                case 1:
                    DisplayAddNewProduct();
                    break;
                case 2:
                    DisplaySpecificProduct();
                    break;
                case 3:
                    DisplayProductList();
                    break;
                case 4:
                    _isApplicationRunning = false;
                    break;
                default:
                    _uIService.PrintErrorMessage("Ogiltigt val, försök igen...");
                    MainMenu();
                    break;

            }

        } while (_isApplicationRunning);

    }

    private void DisplaySpecificProduct()
    {
        throw new NotImplementedException();
    }

    private void DisplayProductList()
    {
        _uIService.NewPage("=== Visa produktlista ===");

        IEnumerable<ProductModel> productList = _productManager.GetAllProducts();

        if (!productList.Any())
        {
            _uIService.PrintErrorMessage("Listan är tom");
        }
        else
        {
            foreach (ProductModel product in productList)
            {
                _uIService.PrintMessage($"Id: {product.Id} - Namn: {product.Name} - Pris: {product.Price} kr");
            }
            _uIService.AddSpacing();
        }
        _uIService.PrintMessage("Tryck på varfri tangent för att återgå till menyn...");
        _uIService.WaitForUserRespons();

    }

    private void DisplayAddNewProduct()
    {
        _uIService.NewPage("=== Lägg till ny produkt ===");


        ProductRequest productRequest = new()
        {
            Name = _uIService.UserInput("Ange namn: "),
            Description = _uIService.UserInput("Ange beskrivning (valbar): ", allowEmpty: true), 
            Price = _uIService.GetNumberInput("Ange pris: ", min: 1)
        };

        bool success = _productManager.SaveProduct(productRequest);
        if (success)
        {
            _uIService.AddSpacing();
            _uIService.PrintMessage($"Produkten {productRequest.Name} lades till.\nTryck på varfri tangent för att återgå till menyn...");
        }
        else
        {
            _uIService.PrintMessage("Något gick fel. Försök igen.");
        }

            _uIService.WaitForUserRespons();
    }
}

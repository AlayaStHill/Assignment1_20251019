using Infrastructure.Interfaces;
using Infrastructure.Models;

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
            List<string> menuOptions = ["Lägg till ny produkt","Visa produktlista", "Avsluta"];
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
                    _uIService.PrintErrorMessage("Ett oväntat fel inträffade...");
                    break;

            }

        } while (_isApplicationRunning);

    }

    private void DisplayProductList()
    {
        _uIService.NewPage("=== Visa produktlista ===");

        IEnumerable<ProductResponse> productList = _productManager.GetAllProducts(); 
        if (!productList.Any())
        {
            _uIService.PrintMessage("Listan är tom");
        }
        else
        {
            foreach (ProductResponse product in productList)
            {
                _uIService.PrintMessage($"Namn: {product.Name} - Beskrivning: {product.Description ?? "Ingen beskrivning"} - Pris: {product.Price} kr");
            }
            _uIService.AddSpacing();
        }
        _uIService.PrintMessage("Tryck på valfri tangent för att återgå till menyn...");
        _uIService.WaitForUserRespons();

    }

    private void DisplayAddNewProduct()
    {
        _uIService.NewPage("=== Lägg till ny produkt ===");


        ProductRequest productRequest = new()
        {
            Name = _uIService.UserInput("Ange namn: "),
            Description = _uIService.UserInputNullable("Ange beskrivning (valbar): "), 
            Price = _uIService.GetNumberInput("Ange pris: ", min: 1)
        };

        bool isSaved = _productManager.SaveProduct(productRequest);
        if (isSaved)
        {
            _uIService.AddSpacing();
            _uIService.PrintMessage($"Produkten {productRequest.Name} lades till.\nTryck på valfri tangent för att återgå till menyn...");
        }
        else
        {
            _uIService.PrintMessage("Något gick fel. Försök igen.");
        }

            _uIService.WaitForUserRespons();
    }
}

using Infrastructure.Models;

namespace Infrastructure.Services;

public class MenuService
{
    private static ProductService _productService = new();
    private bool _isRunning = true;

    public void DisplayMainMenu()
    {
        do
        {
            UIService.NewPage("=== Välkommen till Produkthanteraren ===");
            List<string> options = ["Lägg till ny produkt", "Visa produktlista", "Avsluta"];
            UIService.ShowList(options);

            int optionChoice = UIService.GetNumberInput("Välj ett alternativ: ", min: 1, max: options.Count);

            switch (optionChoice)
            {
                case 1:
                    DisplayAddNewProduct();
                    break;
                case 2:
                    DisplayProductList();
                    break;
                case 3:
                    _isRunning = false;
                    break;
                default:
                    UIService.PrintErrorMessage("Ogiltigt val, försök igen...");
                    DisplayMainMenu();
                    break;

            }

        } while (_isRunning);

    }

    private void DisplayProductList()
    {
        UIService.NewPage("=== Visa produktlista ===");

        IEnumerable<Product> productList = _productService.GetAll();

        foreach (Product product in productList)
        {
            UIService.PrintMessage($"Id: {product.Id} - Namn: {product.Name} - Pris: {product.Price} kr");
        }

        if (productList.Count() == 0)
        {
            UIService.PrintErrorMessage("Listan är tom");
        }
        else
        {
            UIService.AddSpacing();
        }
        UIService.PrintMessage("Tryck på varfri tangent för att återgå till menyn...");
        UIService.WaitForUserRespons();

    }

    private void DisplayAddNewProduct()
    {
        UIService.NewPage("=== Lägg till ny produkt ===");


        Product product = new Product
        {
            Name = UIService.UserInput("Ange namn: "),
            Price = UIService.GetNumberInput("Ange pris: ")
        };

        _productService.CreateProduct(product);

        UIService.AddSpacing();
        UIService.PrintMessage($"Produkten {product.Name} lades till.\nTryck på varfri tangent för att återgå till menyn...");
        UIService.WaitForUserRespons();
    }
}

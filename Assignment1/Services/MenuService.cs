
using Assignment1.Models;

namespace Assignment1.Services;
internal class MenuService
{
    private static ProductService _productService = new();

    public void DisplayMainMenu()
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
                //    case 2:
                //        ShowProductList();
                //        break;
                //    case 3:
                //        Environment.Exit(3);
                //        break;
                //    default:
                //        break;

        }
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

        UIService.PrintMessage($"Produkten {product.Name} lades till (Id: {product.Id})\n Tryck på varfri tangent för att återgå till menyn...");
        UIService.WaitForUserRespons();
    }
}

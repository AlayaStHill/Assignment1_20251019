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
            case 2:
                DisplayProductlist();
                break;
            //case 3:
            //    break;
            //default:
            //    break;

        }
    }

    private void DisplayProductlist()
    {
        UIService.NewPage("=== Visa produktlista ===");

        IEnumerable<Product> productList = _productService.GetAll();

        foreach(Product product in productList)
        {
            UIService.PrintMessage($"Id: {product.Id} - Namn: {product.Name} - Pris: {product.Price} kr");
        }

        if(productList.Count() == 0)
        {
            UIService.PrintErrorMessage("Listan är tom");
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

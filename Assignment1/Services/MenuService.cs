namespace Assignment1.Services;

internal class MenuService
{
    
    internal static void MainMenu()
    {
        Console.WriteLine("=== Välkommen till Produkthanteraren ===");
        List<string> options = ["Lägg till ny produkt", "Visa produktlista", "Avsluta"];
        UIService.ShowList(options);
        Console.WriteLine(""); 
        int optionChoice = UIService.GetNumberInput("Välj ett alternativ: ");

        //switch (optionChoice)
        //{
        //    case 1:
        //        ProductService.AddNewProduct();
        //        break;
        //    case 2:
        //        ProductService.ShowProductList();
        //        break;
        //    case 3:
        //        Environment.Exit(3);
        //        break;
        //    default:
        //        break;

        //}

    }
}

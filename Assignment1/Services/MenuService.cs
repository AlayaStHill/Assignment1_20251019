namespace Assignment1.Services;
// I menuservice har man allt som rör visa-meny och de val/metoder som menyalternativen anger. UIService är samma som MenuService.
// Allt som rör användaren direkt ska skrivas där och input-output
internal class MenuService
{

    internal static void DisplayMainMenu()
    {
        UIService.NewPage("=== Välkommen till Produkthanteraren ===");
        List<string> options = ["Lägg till ny produkt", "Visa produktlista", "Avsluta"];
        UIService.ShowList(options);
      
        int optionChoice = UIService.GetNumberInput("Välj ett alternativ: ");

        //switch (optionChoice)
        //{
        //    case 1:
        //        AddNewProduct();
        //        break;
        //    case 2:
        //        ShowProductList();
        //        break;
        //    case 3:
        //        Environment.Exit(3);
        //        break;
        //    default:
        //        break;

        //}
    }
  



}

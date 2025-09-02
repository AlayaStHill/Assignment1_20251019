

namespace Assignment1.Services;

internal class MenuService
{
    internal static void MainMenu()
    {
        Console.WriteLine("=== Välkommen till Produkthanteraren ===");

        do
        {
            Console.WriteLine("1. Lägg till ny produkt");
            Console.WriteLine("2. Visa produktlista");
            Console.WriteLine("3. Avsluta");
            Console.WriteLine("");
            Console.WriteLine("Välj ett alternativ: ");
            string option = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(option) || )
            {
                Console.WriteLine("Ogiltigt val");
                continue;
            }


            switch ()




        } while (true);


    }
}

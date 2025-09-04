

namespace Assignment1.Services;

internal class UIService
{
    internal static int GetNumberInput(string message, int min = 1, int max = int.MaxValue)
    {
        while (true)
        {
            string numberInput = UserInput(message);
            bool success = int.TryParse(numberInput, out int convertedInput);

            if (!success || convertedInput < min || convertedInput > max)
            {
                Console.WriteLine("Ogiltig inmatning. Ange ett tal");
            }
            else
            {
                return convertedInput;
            }
        }
    }

    internal static void ShowList(List<string> options)
    {
        for(int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
    }

    internal static string UserInput(string message)
    {
        while (true)
        {
            Console.Write(message + " ");
            string userInput = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Ogiltig inmatning. Inmatningen får ej vara tom.");
            }
            else
            {
                return userInput;
            }
        }
    }
}

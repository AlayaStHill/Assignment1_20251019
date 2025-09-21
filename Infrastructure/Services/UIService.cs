using Infrastructure.Helpers;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class UIService : IUIService
{
    public int GetNumberInput(string message, int min = 1, int max = int.MaxValue)
    {
        while (true)
        {
            string userInput = UserInput(message);

            if (!NumberInputValidator.IsValid(userInput, min, max, out int validNumber))
            {
                PrintErrorMessage("Ogiltig inmatning. Ange ett tal");
            }
            else
            {
                return validNumber;
            }
        }
    }

    public void ShowList(List<string> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.WriteLine("");
    }

    public string UserInput(string message)
    {
        while (true)
        {
            Console.Write(message);
            string userInput = Console.ReadLine()!;

            if (!StringInputValidator.IsValid(userInput))
            {
                PrintErrorMessage("Ogiltig inmatning. Inmatningen får ej vara tom.");
            }
            else
            {
                return userInput.Trim();
            }
        }
    }

    public string? UserInputNullable(string message)
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        // Ternary operator: condition ? valueReturnedIfTrue : valueReturnedIfFalse
        return string.IsNullOrWhiteSpace(input) ? null : input;
    }


    public void NewPage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        AddSpacing();
    }

    public void PrintErrorMessage(string errorMessage)
    {
        AddSpacing();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ForegroundColor = ConsoleColor.White;
        AddSpacing();
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void WaitForUserRespons()
    {
        Console.ReadKey();
    }

    public void AddSpacing()
    {
        Console.WriteLine("");
    }
}

using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class UIService : IUIService
{
    public int GetNumberInput(string message, int min = 1, int max = int.MaxValue)
    {
        while (true)
        {
            string numberInput = UserInput(message);
            bool success = int.TryParse(numberInput, out int convertedInput);

            if (!success || convertedInput < min || convertedInput > max)
            {
                PrintErrorMessage("Ogiltig inmatning. Ange ett tal");
            }
            else
            {
                return convertedInput;
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

            if (string.IsNullOrWhiteSpace(userInput))
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
        return Console.ReadLine();
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

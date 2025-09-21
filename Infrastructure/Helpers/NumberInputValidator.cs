namespace Infrastructure.Helpers;

public static class NumberInputValidator
{
    public static bool IsValid(string input, int min, int max, out int parsedNumber)
    {
        bool success = int.TryParse(input, out parsedNumber);

        if (!success)
        {
            return false;
        }

        if (parsedNumber < min || parsedNumber > max)
        {
            return false;
        }

        return true;
        
    }
}

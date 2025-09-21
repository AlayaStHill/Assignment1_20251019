namespace Infrastructure.Helpers;

public static class StringInputValidator
{
    public static bool IsValid(string input) 
    {

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        return true;
        
    }
}

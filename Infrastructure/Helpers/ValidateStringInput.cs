namespace Infrastructure.Helpers;

public static class ValidateStringInput
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

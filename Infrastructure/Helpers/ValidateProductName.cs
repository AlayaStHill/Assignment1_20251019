using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class ValidateProductName
{
    public static bool IsValid(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return true;
    }             
}   

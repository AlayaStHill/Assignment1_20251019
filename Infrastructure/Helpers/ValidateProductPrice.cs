using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class ValidateProductPrice
{
    public static bool IsValid(decimal price)
    {
        if (price <= 0)
        {
            return false;
        }

        return true;
    }
            
}

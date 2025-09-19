using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class ProductPriceValidator
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

using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class ProductRequestValidator
{
    public static bool IsValid(ProductRequest productRequest)
    {
        if (productRequest == null)
        {
            return false;
        }
        return true;
            
    }  
   
}

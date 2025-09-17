using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class ValidateProductRequest
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

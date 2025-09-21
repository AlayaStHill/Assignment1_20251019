using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class ProductNameValidator // ProductNameValidator istället
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

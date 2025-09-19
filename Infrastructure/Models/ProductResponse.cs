namespace Infrastructure.Models;

public class ProductResponse
{
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

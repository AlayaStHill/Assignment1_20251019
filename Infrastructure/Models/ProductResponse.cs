namespace Infrastructure.Models;

public class ProductResponse
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

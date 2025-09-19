namespace Infrastructure.Models;

public class ProductRequest
{
    public string Id { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

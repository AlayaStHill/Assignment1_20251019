namespace Infrastructure.Models;

public class ProductResponse
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; } // - skrivs ut: $"Beskrivning: {productRequest.Description ?? "Ingen"}"
    public decimal Price { get; set; }
}

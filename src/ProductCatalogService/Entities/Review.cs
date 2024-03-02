namespace ProductCatalogService.Entities;

public class Review
{
  public required string CustomerId { get; set; } 
  public int Rating { get; set; }
  public string Comment { get; set; } = null!;
}

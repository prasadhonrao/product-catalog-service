namespace ProductCatalogService.Models;

public class BasicProductModel
{
  public Guid Id { get; set; } 
  public string ProductName { get; set; } = string.Empty;
  public string ProductDescription { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public int Quantity { get; set; }
}

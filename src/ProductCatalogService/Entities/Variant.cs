namespace ProductCatalogService.Entities;

public class Variant
{
  public required string Id { get; set; }
  public string Color { get; set; } = string.Empty;
  public string Size { get; set; } = string.Empty;
  public int Quantity { get; set; }
}

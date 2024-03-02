namespace ProductCatalogService.Entities;

public class Rating
{
  public required string CustomerId { get; set; }
  public int RatingValue { get; set; }
}

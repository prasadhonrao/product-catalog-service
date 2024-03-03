namespace ProductCatalogService.Entities;

public class Rating
{
  public required string CustomerId { get; set; }
  public double RatingValue { get; set; }
}

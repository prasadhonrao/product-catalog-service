namespace ProductCatalogService.Models;

public class ProductModel
{
  public Guid Id { get; set; }
  public string ProductName { get; set; } = string.Empty;
  public string ProductDescription { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public Status Status { get; set; }
  public InventoryStatus InventoryStatus { get; set; }
  public Seo Seo { get; set; } = null!;
  public ICollection<Category> Categories { get; set; } = null!;
  public ICollection<Specification> Specifications { get; set; } = null!;
  public ICollection<Image> Images { get; set; } = null!;
  public ICollection<Variant> Variants { get; set; } = null!;
  public List<BasicProductModel>? RelatedProducts { get; set; } 
  public ICollection<Review> Reviews { get; set; } = null!;
  public ICollection<Rating> Ratings { get; set; } = null!;
  public double AggregateRating => Ratings != null && Ratings.Count > 0 ? Ratings.Average(r => r.RatingValue) : 0;
  public long ReviewCount => Reviews != null ? Reviews.Count : 0;
}

using ProductCatalogService.Entities;

namespace ProductCatalogService.Models;

public class ProductModel
{
  public int Id { get; set; }
  public string ProductName { get; set; } = string.Empty;
  public string ProductDescription { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public string Status { get; set; } = string.Empty;
  public string InventoryStatus { get; set; } = string.Empty;
  public Seo Seo { get; set; } = null!;
  public ICollection<Category> Categories { get; set; } = null!; 
  public ICollection<Specification> Specifications { get; set; } = null!;
  public ICollection<Image> Images { get; set; } = null!;
  public ICollection<Variant> Variants { get; set; } = null!;
  public ICollection<string> RelatedProducts { get; set; } = null!;
  public ICollection<Review> Reviews { get; set; } = null!;
  public ICollection<Rating> Ratings { get; set; } = null!;
  public AggregateRating AggregateRating { get; set; } = null!;
  public AggregateReview AggregateReview { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalogService.Entities;

public class Product
{

  [Key]
  public string Id { get; set; } = null!;

  [Required(ErrorMessage = "Product name is required"), MaxLength(100)]
  public required string ProductName { get; set; }

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
  public ICollection<int> RelatedProducts { get; set; } = null!;
  public ICollection<Review> Reviews { get; set; } = null!;
  public ICollection<Rating> Ratings { get; set; } = null!;
  public decimal AggregateRating { get; set; } 
  public decimal AggregateReview { get; set; } 
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductCatalogService.Entities;

public class Product
{

  [Key]
  public Guid Id { get; set; }

  [Required(ErrorMessage = "Product name is required"), MaxLength(100)]
  public required string ProductName { get; set; }

  public string ProductDescription { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public string Status { get; set; } = string.Empty;
  public string InventoryStatus { get; set; } = string.Empty;
  public Seo Seo { get; set; } = null!;
  [JsonIgnore]
  public ICollection<Category>? Categories { get; set; }
  public ICollection<Specification> Specifications { get; set; } = null!;
  public ICollection<Image> Images { get; set; } = null!;
  public ICollection<Variant> Variants { get; set; } = null!;
  public HashSet<ProductRelation> RelatedProducts { get; set; } = new();
  public ICollection<Review>? Reviews { get; set; }
  public ICollection<Rating>? Ratings { get; set; }
  public decimal AggregateRating { get; set; }
  public long ReviewCount { get; set; }
}

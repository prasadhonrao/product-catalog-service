using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalogService.Entities;

public class Rating
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  public string CustomerId { get; set; } = null!;
  public double RatingValue { get; set; }
  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalogService.Entities;

public class Review
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  public string CustomerId { get; set; } = null!;
  public string Comment { get; set; } = null!;
  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;
}

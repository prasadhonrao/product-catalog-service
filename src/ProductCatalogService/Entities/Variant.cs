using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogService.Entities;

public class Variant
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  public string Color { get; set; } = string.Empty;
  public string Size { get; set; } = string.Empty;
  public int Quantity { get; set; }

  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;
}

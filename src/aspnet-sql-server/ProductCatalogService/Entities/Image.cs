using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogService.Entities;

public class Image
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  public string ImageUrl { get; set; } = string.Empty;
  public string ImageDescription { get; set; } = string.Empty;
  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;
}

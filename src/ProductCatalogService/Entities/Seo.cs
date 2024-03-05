using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogService.Entities;

public class Seo
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  public string MetaTitle { get; set; } = string.Empty;
  public string MetaDescription { get; set; } = string.Empty;
  public string Slug { get; set; } = string.Empty;
  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogService.Entities;

public class Specification
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;
  public string Value { get; set; } = string.Empty;

  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;
}


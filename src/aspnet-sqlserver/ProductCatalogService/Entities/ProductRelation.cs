using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductRelation
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;

  public Guid RelatedProductId { get; set; }
  public Product RelatedProduct { get; set; } = null!;
}

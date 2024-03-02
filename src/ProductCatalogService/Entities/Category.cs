using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalogService.Entities;

public class Category
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Key]
  public int Id { get; set; }

  [Required(ErrorMessage = "Category name is required"), MaxLength(25)]
  public required string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
}
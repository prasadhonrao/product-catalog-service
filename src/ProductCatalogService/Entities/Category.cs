using System.ComponentModel.DataAnnotations;

namespace ProductCatalogService.Entities;

public class Category
{
  [Key]
  public Guid Id { get; private set; }

  [Required(ErrorMessage = "Category name is required"), MaxLength(25, ErrorMessage = "Name should be less than 25 characters")]
  public string Name { get; set; } = string.Empty;

  [Required(ErrorMessage = "Category name is required"), MaxLength(100, ErrorMessage = "Description should be less than 100 characters")]
  public string Description { get; set; } = string.Empty;

  public Category()
  {
    Id = Guid.NewGuid();
  }
}
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogService.Models;

public class CategoryModel
{
  public Guid Id { get; set; }

  [Required(ErrorMessage = "Category name is required"), MaxLength(25, ErrorMessage = "Name should be less than 25 characters")]
  public string Name { get; set; } = string.Empty;

  [Required(ErrorMessage = "Category description is required"), MaxLength(100, ErrorMessage = "Description should be less than 100 characters")]
  public string Description { get; set; } = string.Empty;
  public List<BasicProductModel>? Products { get; set; }
  public int ProductCount => Products?.Count ?? 0;

}

using System.ComponentModel.DataAnnotations;

namespace ProductCatalogService.Models;

public class CategoryModel
{
  public Guid Id { get; set; }

  [Required(ErrorMessage = "Category name is required"), MaxLength(25, ErrorMessage = "Name should be less than 25 characters")]
  public string Name { get; set; }

  [Required(ErrorMessage = "Category description is required"), MaxLength(100, ErrorMessage = "Description should be less than 100 characters")]
  public string Description { get; set; }
  public ICollection<Product> Products { get; set; }
  public int ProductCount => Products.Count;

  public CategoryModel(string name, string description)
  {
    Name = name;
    Description = description;
    Products = new HashSet<Product>();
  }
}

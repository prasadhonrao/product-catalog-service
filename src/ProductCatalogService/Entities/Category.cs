using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalogService.Entities;

public class Category
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; private set; }

  [Required(ErrorMessage = "Category name is required"), MaxLength(25, ErrorMessage = "Name should be less than 25 characters")]
  public string Name { get; set; } = null!;

  [Required(ErrorMessage = "Category description is required"), MaxLength(100, ErrorMessage = "Description should be less than 100 characters")]
  public string Description { get; set; } = null!;
  public ICollection<Product> Products { get; set; } = new HashSet<Product>();

  public Category()
  {
  }
  public Category(string name, string description)
  {
    Name = name;
    Description = description;
  }
}
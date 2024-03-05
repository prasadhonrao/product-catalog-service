namespace ProductCatalogService;

public class DataInitializerService
{
  private readonly ProductCatalogServiceContext _context;

  public DataInitializerService(ProductCatalogServiceContext context)
  {
    _context = context;
  }

  public async Task SeedAsync()
  {
    // Create the database if it doesn't exist
    await _context.Database.EnsureCreatedAsync();

    // Check if any categories exist
    if (!_context.Categories.Any())
    {
      // Seed categories
      var electronicsCategory = new Category { Name = "Electronics", Description = "Electronic devices" };
      var clothingCategory = new Category { Name = "Clothing", Description = "Clothing items" };
      var booksCategory = new Category { Name = "Books", Description = "Books and literature" };
      var sportsCategory = new Category { Name = "Sports", Description = "Sports Equipments" };

      _context.Categories.AddRange(electronicsCategory, clothingCategory, booksCategory, sportsCategory);

      await _context.SaveChangesAsync();

      // Seed products
      var product1 = new Product { ProductName = "iPhone 12", Categories = new List<Category> { electronicsCategory } };
      var product2 = new Product { ProductName = "Levi's Jeans", Categories = new List<Category> { clothingCategory } };
      var product3 = new Product { ProductName = "Harry Potter and the Philosopher's Stone", Categories = new List<Category> { booksCategory } };
      var product4 = new Product { ProductName = "Wireless Headphone for Running", Categories = new List<Category> { electronicsCategory, sportsCategory } };

      _context.Products.AddRange(product1, product2, product3, product4);

      await _context.SaveChangesAsync();
    }
  }
}

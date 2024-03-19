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
    // Drop the database if it exists
    //await _context.Database.EnsureDeletedAsync();

    // Create new database
    //await _context.Database.EnsureCreatedAsync();

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
      _context.Products.Add(product1);
      await _context.SaveChangesAsync();

      var product2 = new Product { ProductName = "Levi's Jeans", Categories = new List<Category> { clothingCategory } };
      var product3 = new Product { ProductName = "Harry Potter and the Philosopher's Stone", Categories = new List<Category> { booksCategory } };
      var product4 = new Product { ProductName = "Wireless Headphone for Running", Categories = new List<Category> { electronicsCategory, sportsCategory } };

      // Create new product iPhone 15 and add iPhone 12 as related products
      var product5 = new Product { ProductName = "iPhone 15", Categories = new List<Category> { electronicsCategory } };
      product5.RelatedProducts = new HashSet<ProductRelation> { new ProductRelation { RelatedProductId = product1.Id } };

      _context.Products.AddRange(product2, product3, product4, product5);
      await _context.SaveChangesAsync();
    }
  }
}

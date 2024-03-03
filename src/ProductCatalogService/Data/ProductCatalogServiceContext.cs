namespace ProductCatalogService.Data;

public class ProductCatalogServiceContext : DbContext
{
  public ProductCatalogServiceContext(DbContextOptions<ProductCatalogServiceContext> options)
      : base(options)
  {
  }
    
  public DbSet<Category> Category { get; set; } = default!;
}

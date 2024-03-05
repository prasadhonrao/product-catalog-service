namespace ProductCatalogService.Data;

public class ProductCatalogServiceContext : DbContext
{
  public ProductCatalogServiceContext(DbContextOptions<ProductCatalogServiceContext> options)
      : base(options)
  {
  }
    
  public DbSet<Category> Categories { get; set; } = default!;
  public DbSet<Product> Products { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Define many-to-many relationship between Product and Category
    modelBuilder.Entity<ProductRelation>()
      .HasOne(pr => pr.Product)
      .WithMany(p => p.ProductRelations)
      .HasForeignKey(pr => pr.ProductId);

    modelBuilder.Entity<ProductRelation>()
      .HasOne(pr => pr.RelatedProduct)
      .WithMany()
      .HasForeignKey(pr => pr.RelatedProductId)
      .OnDelete(DeleteBehavior.Restrict);

    // Handle decimal precision warning
    modelBuilder.Entity<Product>()
      .Property(p => p.Price)
      .HasPrecision(10, 2);

    modelBuilder.Entity<Product>()
      .Property(p => p.AggregateRating)
      .HasPrecision(2, 1);
   
  }

}

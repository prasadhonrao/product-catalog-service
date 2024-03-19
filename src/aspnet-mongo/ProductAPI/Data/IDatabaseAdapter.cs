using ProductAPI.Models;

namespace ProductAPI.Data;

public interface IDatabaseAdapter
{
    // Category methods
    Task<List<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryAsync(string id);
    Task<TransactionResult> CreateCategoryAsync(Category category);
    Task<TransactionResult> UpdateCategoryAsync(string id, Category category);
    Task<TransactionResult> DeleteCategoryAsync(string id);

    // Product methods
    Task<List<Product>> GetProductsAsync();
    Task<Product> GetProductAsync(string id);
    Task<TransactionResult> CreateProductAsync(Product product);
    Task<TransactionResult> UpdateProductAsync(string id, Product product);
    Task<TransactionResult> DeleteProductAsync(string id);

    // Product and Category methods
    Task<List<Product>> GetProductsWithCategoryAsync(string categoryId);
    Task<TransactionResult> AddProductWithCategoriesAsync(Product product, List<string> categoryIds);
    Task<TransactionResult> AddNewCategoryToProductAsync(string productId, Category category);
    Task<TransactionResult> UpdateProductCategoriesAsync(string productId, List<string> categoryIds);
    Task<TransactionResult> DeleteCategoryFromAProductAsync(string productId, string categoryId);
    Task<TransactionResult> DeleteAllCategoriesFromProductAsync(string productId);
}

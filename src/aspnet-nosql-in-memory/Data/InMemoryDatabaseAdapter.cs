using ProductCatalogAPI.Models;
using System.Collections.Concurrent;

namespace ProductCatalogAPI.Data
{
    public class InMemoryDatabaseAdapter : IDatabaseAdapter
    {
        private readonly ConcurrentDictionary<string, Category> categories = new ConcurrentDictionary<string, Category>();
        private readonly ConcurrentDictionary<string, Product> products = new ConcurrentDictionary<string, Product>();

        // Category methods
        public Task<List<Category>> GetCategoriesAsync()
        {
            return Task.FromResult(categories.Values.ToList());
        }

        public Task<Category> GetCategoryAsync(string id)
        {
            categories.TryGetValue(id, out var category);
            return Task.FromResult(category);
        }

        public Task<TransactionResult> CreateCategoryAsync(Category category)
        {
            if (category == null)
            {
                return Task.FromResult(TransactionResult.BadRequest);
            }

            try
            {
                category.Id = Guid.NewGuid();
                categories[category.Id.ToString()] = category;
                return Task.FromResult(TransactionResult.Success);
            }
            catch (Exception)
            {
                // Log the exception here
                return Task.FromResult(TransactionResult.InternalServerError);
            }
        }


        public Task<TransactionResult> UpdateCategoryAsync(string id, Category category)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> DeleteCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> UpdateProductAsync(string id, Product product)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsWithCategoryAsync(string categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> AddProductWithCategoriesAsync(Product product, List<string> categoryIds)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> AddNewCategoryToProductAsync(string productId, Category category)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> UpdateProductCategoriesAsync(string productId, List<string> categoryIds)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> DeleteCategoryFromAProductAsync(string productId, string categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> DeleteAllCategoriesFromProductAsync(string productId)
        {
            throw new NotImplementedException();
        }
    }
}
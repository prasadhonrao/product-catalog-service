using MongoDB.Bson;
using MongoDB.Driver;
using ProductAPI.Models;

namespace ProductAPI.Data;

public class MongoDbDatabase : IDatabaseAdapter
{
    private readonly IMongoCollection<Category> categories;
    private readonly IMongoCollection<Product> products;

    public MongoDbDatabase(IMongoClient client)
    {
        var database = client.GetDatabase("ProductsDB");
        categories = database.GetCollection<Category>("categories");
        products = database.GetCollection<Product>("products");
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await categories.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Category> GetCategoryAsync(string id)
    {
        var filter = Builders<Category>.Filter.Eq("id", id.ToString());
        return await categories.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<TransactionResult> CreateCategoryAsync(Category category)
    {
        try
        {
            await categories.InsertOneAsync(category);

            if (category.ObjectId != null)
            {
                return TransactionResult.Success;
            }
            return TransactionResult.BadRequest;
        }
        catch (Exception)
        {
            return TransactionResult.InternalServerError;
        }
    }

    public async Task<TransactionResult> UpdateCategoryAsync(string id, Category category)
    {
        try
        {
            var filter = Builders<Category>.Filter.Eq("id", id);
            var update = Builders<Category>.Update
                .Set("name", category.Name)
                .Set("description", category.Description);
            var result = await categories.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                return TransactionResult.NotFound;
            }

            if (result.ModifiedCount > 0)
            {
                return TransactionResult.Success;
            }
            return TransactionResult.BadRequest;
        }
        catch (Exception)
        {
            return TransactionResult.InternalServerError;
        }
    }

    public async Task<TransactionResult> DeleteCategoryAsync(string id)
    {
        try
        {
            var filter = Builders<Category>.Filter.Eq("id", id);
            var result = await categories.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                return TransactionResult.NotFound;
            }

            if (result.DeletedCount > 0)
            {
                return TransactionResult.Success;
            }
            return TransactionResult.BadRequest;
        }
        catch (Exception)
        {
            return TransactionResult.InternalServerError;
        }
        
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await products.Find(_ => true).ToListAsync();
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

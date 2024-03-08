using Microsoft.AspNetCore.JsonPatch;

namespace ProductCatalogService.Repositories;
public interface ICategoryRepository
{
  Task<IEnumerable<Category>> GetCategories(string? nameLike, bool includeProducts = false, int pageNumber = 1, int pageSize = 10);
  Task<Category?> GetCategory(Guid id);
  Task AddCategory(Category category);
  Task UpdateCategory(Guid id, Category category);
  Task PatchCategory(Guid id, Category patch);
  Task DeleteCategory(Guid id);
  Task<bool> CategoryExists(Guid id);
}

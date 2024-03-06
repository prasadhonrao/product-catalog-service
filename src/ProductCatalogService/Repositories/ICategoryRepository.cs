using Microsoft.AspNetCore.JsonPatch;

namespace ProductCatalogService.Repositories;
public interface ICategoryRepository
{
  Task<IEnumerable<Category>> GetCategories(bool includeProducts = false);
  Task<Category?> GetCategory(Guid id);
  Task AddCategory(Category category);
  Task UpdateCategory(Guid id, Category category);
  Task PatchCategory(Guid id, Category patch);
  Task DeleteCategory(Guid id);
  Task<bool> CategoryExists(Guid id);
}

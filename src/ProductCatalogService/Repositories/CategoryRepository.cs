namespace ProductCatalogService.Repositories;

public class CategoryRepository : ICategoryRepository
{
  private readonly ProductCatalogServiceContext context;

  public CategoryRepository(ProductCatalogServiceContext context)
  {
    this.context = context;
  }

  public async Task AddCategory(Category category)
  {
    try
    {
      await context.Categories.AddAsync(category);
      await context.SaveChangesAsync();
    }
    catch (DbUpdateException ex)
    {
      throw new RepositoryException<CategoryRepository>("An error occurred while adding the category to the database.", ex);
    }
  }

  public async Task<bool> CategoryExists(Guid id)
  {
    return await context.Categories.AnyAsync(c => c.Id == id);
  }

  public async Task DeleteCategory(Guid id)
  {
    try
    {
      var category = await context.Categories.FindAsync(id);
      if (category == null)
      {
        throw new KeyNotFoundException($"Category with id {id} not found.");
      }
      context.Categories.Remove(category);
      await context.SaveChangesAsync();
    }
    catch (DbUpdateException ex)
    {
      throw new RepositoryException<CategoryRepository>("An error occurred while deleting the category from the database.", ex);
    }
  }

  public async Task<IEnumerable<Category>> GetCategories(string? nameLike, bool includeProducts = false)
  {
    try
    {
      IQueryable<Category> query = context.Categories;

      if (!string.IsNullOrWhiteSpace(nameLike))
      {
        query = query.Where(c => c.Name.Contains(nameLike, StringComparison.InvariantCultureIgnoreCase));
      }

      if (includeProducts)
      {
        query = query.Include(c => c.Products);
      }
      return await query.ToListAsync();
    }
    catch (DbUpdateException ex)
    {
      throw new RepositoryException<CategoryRepository>("An error occurred while getting the categories from the database.", ex);
    }
  }

  public async Task<Category?> GetCategory(Guid id)
  {
    try
    {
      return await context.Categories.FindAsync(id);
    }
    catch (DbUpdateException ex)
    {
      throw new RepositoryException<CategoryRepository>("An error occurred while getting getting category from the database.", ex);
    }
  }

  public async Task PatchCategory(Guid id, Category updatedCategory)
  {
    // Check if category exists in the database
    var category = await context.Categories.FindAsync(id);
    if (category == null)
    {
      throw new RepositoryException<CategoryRepository>($"Category with id {id} not found.");
    }
    // Update the category
    category.Name = updatedCategory.Name;
    category.Description = updatedCategory.Description;

    try
    {
      await context.SaveChangesAsync();
    }
    catch (DbUpdateException ex)
    {
      throw new RepositoryException<CategoryRepository>("An error occurred while updating the category in the database.", ex);
    }
  }

  public async Task UpdateCategory(Guid id, Category updatedCategory)
  {
    // Check if category exists in the database
    var category = await context.Categories.FindAsync(id);
    if (category == null)
    {
      throw new RepositoryException<CategoryRepository>($"Category with id {id} not found.");
    }

    // Update the category
    category.Name = updatedCategory.Name;
    category.Description = updatedCategory.Description;

    try
    {
      await context.SaveChangesAsync();
    }
    catch (DbUpdateException ex)
    {
      throw new RepositoryException<CategoryRepository>("An error occurred while updating the category in the database.", ex);
    }
  }
}

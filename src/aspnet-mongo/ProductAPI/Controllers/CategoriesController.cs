using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Controllers;
[Route("api/products/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IDatabaseAdapter databaseAdapter;

    public CategoriesController(IDatabaseAdapter databaseAdapter)
    {
        this.databaseAdapter = databaseAdapter;
    }

    [HttpGet(Name = "GetAllCategories")]
    public async Task<ActionResult<List<Category>>> GetCategoriesAsync()
    {
        return await databaseAdapter.GetCategoriesAsync();
    }

    [HttpGet("{id}", Name = "GetCategory")]
    public async Task<ActionResult<Category>> GetCategoryAsync(string id)
    {
        var category = await databaseAdapter.GetCategoryAsync(id);
        if (category is null)
        {
            return NotFound();
        }
        return category;
    }

    [HttpPost(Name = "CreateCategory")]
    public async Task<ActionResult<Category>> CreateCategoryAsync(Category category)
    {
        category.Guid = Guid.NewGuid().ToString();    
        await databaseAdapter.CreateCategoryAsync(category);
        return CreatedAtRoute("GetCategory", new { id = category.Guid }, category);
    }

    [HttpPut("{id}", Name = "UpdateCategory")]
    public async Task<IActionResult> UpdateCategoryAsync(string id, Category category)
    {
        // check if the category exists
        var existingCategory = await databaseAdapter.GetCategoryAsync(id);
        if (existingCategory is null)
        {
            return NotFound();
        }

        // update the category
        await databaseAdapter.UpdateCategoryAsync(id, category);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteCategory")]
    public async Task<IActionResult> DeleteCategoryAsync(string id)
    {
        // check if the category exists
        var existingCategory = await databaseAdapter.GetCategoryAsync(id);
        if (existingCategory is null)
        {
            return NotFound();
        }
        await databaseAdapter.DeleteCategoryAsync(id);
        return NoContent();
    }
}

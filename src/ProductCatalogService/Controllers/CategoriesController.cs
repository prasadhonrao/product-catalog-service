using Microsoft.AspNetCore.JsonPatch;
using ProductCatalogService.Models;
using ProductCatalogService.Repositories;

namespace ProductCatalogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
  private readonly ILogger<CategoriesController> logger;
  private readonly ICategoryRepository repository;

  public CategoriesController(ICategoryRepository repository, ILogger<CategoriesController> logger)
  {
    this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories([FromQuery] bool includeProducts = false)
  {

    logger.LogInformation("Getting all categories");
    var categories = await repository.GetCategories(includeProducts);

    var categoryModels = categories.Select(category => new CategoryModel
    {
      Id = category.Id,
      Name = category.Name,
      Description = category.Description,
      Products = includeProducts ? category.Products.Select(product => new BasicProductModel
      {
        Id = product.Id,
        ProductName = product.ProductName,
        ProductDescription = product.ProductDescription,
        Price = product.Price,
        Quantity = product.Quantity
        }).ToList() : null
    });

    return Ok(categoryModels);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<CategoryModel>> GetCategory(Guid id)
  {
    logger.LogInformation($"Getting category with id: {id}");
    var category = await repository.GetCategory(id);

    if (category == null)
    {
      return NotFound();
    }

    return Ok(category);
  }

  [HttpPost]
  public async Task<ActionResult<Category>> PostCategory(CategoryModel categoryModel)
  {
    logger.LogInformation("Creating a new category");

    // Check if the model is valid
    if (categoryModel == null)
    {
      logger.LogError("Invalid category model");
      return BadRequest();
    }

    if (!ModelState.IsValid)
    {
      logger.LogError("Invalid category model");
      return BadRequest(ModelState);
    }

    // Map the model to the entity
    var category = new Category(description: categoryModel.Description,
                                name: categoryModel.Name);

    await repository.AddCategory(category);
    return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
  }

  [HttpPut("{id:guid}")]
  public async Task<IActionResult> PutCategory(Guid id, CategoryModel categoryModel)
  {
    logger.LogInformation($"Updating category with id: {id}");
    // Check if the Category Guid is valid
    ValidateGuid(id);

    // Check if the model is valid
    if (categoryModel == null)
    {
      logger.LogError("Invalid category model");
      return BadRequest();
    }

    if (!ModelState.IsValid)
    {
      logger.LogError("Invalid category model");
      return BadRequest(ModelState);
    }

    // Map the model to the entity
    var category = new Category
    {
      Name = categoryModel.Name,
      Description = categoryModel.Description
    };

    // Call the repository method to update the category
    await repository.UpdateCategory(id, category);
    return NoContent();
  }

  [HttpPatch("{id:guid}")]
  public async Task<IActionResult> PatchCategory(Guid id, JsonPatchDocument<CategoryModel> patchDocument)
  {

    logger.LogInformation($"Patching category with id: {id}");

    // Check if the Category Guid is valid
    ValidateGuid(id);

    // Check if the model is valid
    if (patchDocument == null)
    {
      logger.LogError("Invalid patch document");
      return BadRequest();
    }

    if (!ModelState.IsValid)
    {
      logger.LogError("Invalid patch document");
      return BadRequest(ModelState);
    }

    // Get the category from the repository
    var category = await repository.GetCategory(id);
    if (category == null)
    {
      logger.LogError($"Category with id: {id} not found");
      return NotFound();
    }

    var patchModel = new CategoryModel { Name = category.Name, Description = category.Description };

    // Apply the patch operations to the patchModel
    patchDocument.ApplyTo(patchModel, error =>
    {
      ModelState.AddModelError("", error.ErrorMessage);
    });

    if (!ModelState.IsValid)
    {
      logger.LogError("Invalid patch document");
      return BadRequest(ModelState);
    }

    if (!TryValidateModel(patchModel))
    {
      logger.LogError("Invalid patch document");
      return BadRequest(ModelState);
    }

    // Update the category with the patched values
    category.Name = patchModel.Name;
    category.Description = patchModel.Description;

    await repository.PatchCategory(id, category);

    return NoContent();
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteCategory(Guid id)
  {
    logger.LogInformation($"Deleting category with id: {id}");

    // Check if the Category Guid is valid
    ValidateGuid(id);

    await repository.DeleteCategory(id);

    return NoContent();
  }

  private void ValidateGuid(Guid id)
  {
    if (id == Guid.Empty)
    {
      throw new ArgumentException($"Invalid category id: {id}");
    }
  }
}

using Microsoft.AspNetCore.JsonPatch;
using ProductCatalogService.Models;
using System.Reflection;


namespace ProductCatalogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
  private readonly ProductCatalogServiceContext context;
  private readonly ILogger<CategoriesController> logger;

  public CategoriesController(ProductCatalogServiceContext context, ILogger<CategoriesController> logger)
  {
    this.context = context ?? throw new ArgumentNullException(nameof(context)) ;
    this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  // GET: api/Categories
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
  {
    try
    {
      logger.LogInformation("Getting all categories");
      return await context.Categories.ToListAsync();
    }
    catch (Exception ex)
    {
      logger.LogError(ex,"An error occurred while getting all categories.");
      return StatusCode(500, "An error occurred while getting all categories. Please try again later.");
    }
  }

  // GET: api/Categories/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Category>> GetCategory(Guid id)
  {
    try
    {
      logger.LogInformation($"Getting category with id: {id}");
      var category = await context.Categories.FindAsync(id);

      if (category == null)
      {
        return NotFound();
      }

      return category;
    }
    catch(Exception ex)
    {
      logger.LogError(ex, "An error occurred while getting the category.");
      return StatusCode(500, "An error occurred while getting the category. Please try again later.");
    }
  }

  // PUT: api/Categories/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPut("{id}")]
  public async Task<IActionResult> PutCategory(Guid id, CategoryModel categoryModel)
  {
    logger.LogInformation($"Updating category with id: {id}");

    // Check if the Category Guid is valid
    ValidateId(id);

    // Check if category exists in the database
    var category = await context.Categories.FindAsync(id);
    if (category == null)
    {
      logger.LogError($"Category with id: {id} not found");
      return NotFound();
    }

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
    category.Name = categoryModel.Name;
    category.Description = categoryModel.Description;

    // Update the entity
    context.Entry(category).State = EntityState.Modified;

    try
    {
      await context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!CategoryExists(id))
      {
        return NotFound();
      }
      else
      {
        logger.LogError("An error occurred while updating the category.");
        throw;
      }
    }

    return NoContent();
  }

  // POST: api/Categories
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
    

    try
    {
      context.Categories.Add(category);
      await context.SaveChangesAsync();
    }
    catch (DbUpdateException dbex)
    {
      if (CategoryExists(category.Id))
      {
        return Conflict();
      }
      else
      {
        logger.LogError(dbex, "An error occurred while creating the category.");
        throw;
      }
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "An error occurred while creating a category");
      return StatusCode(500, "An error occurred while creating a category. Please try again later.");
    }

    return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
  }

  // PATCH: api/Categories/5
  [HttpPatch("{id}")]
  public async Task<IActionResult> PatchCategory(Guid id, JsonPatchDocument<CategoryModel> patchDocument)
  {
    try
    {
      logger.LogInformation($"Patching category with id: {id}");

      // Check if the Category Guid is valid
      ValidateId(id);

      // Check if category exists in the database
      var category = await context.Categories.FindAsync(id);
      if (category == null)
      {
        logger.LogError($"Category with id: {id} not found");
        return NotFound();
      }

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

      var patchModel = new CategoryModel
      (
        category.Name,
        category.Description
      );

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

      // Save the changes to the database
      context.Entry(category).State = EntityState.Modified;
      await context.SaveChangesAsync();

      return NoContent();
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "An error occurred while patching the category.");
      return StatusCode(500, "An error occurred while patching the category. Please try again later.");
    }
  }

  // DELETE: api/Categories/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCategory(Guid id)
  {
    var category = await context.Categories.FindAsync(id);
    if (category == null)
    {
      return NotFound();
    }

    try
    {
      context.Categories.Remove(category);
      await context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
      logger.LogError(ex,"An error occurred while deleting the category.");
      return StatusCode(500, "An error occurred while patching the category. Please try again later.");
    }

    return NoContent();
  }

  private bool CategoryExists(Guid id)
  {
    return context.Categories.Any(e => e.Id == id);
  }

  private void ValidateId(Guid id)
  {
    if (id == Guid.Empty)
    {
      throw new ArgumentException($"Invalid category id: {id}");
    }
  }

  private void ApplyPatchOperation(CategoryModel patchModel, string op, string path, string value)
  {
    var propertyPath = path.TrimStart('/');
    var propertyNames = propertyPath.Split('/');

    var targetType = typeof(CategoryModel);
    var targetProperty = GetPropertyInHierarchy(targetType, propertyNames[0], ignoreCase: true);
    if (targetProperty != null)
    {
      switch (op.ToLowerInvariant())
      {
        case "add":
        case "replace":
          var valueToSet = Convert.ChangeType(value, targetProperty.PropertyType);
          targetProperty.SetValue(patchModel, valueToSet);
          break;

        case "remove":
          targetProperty.SetValue(patchModel, null);
          break;

        case "copy":
          var sourceProperty = GetPropertyInHierarchy(targetType, value, ignoreCase: true);

          if (sourceProperty != null)
          {
            var sourceValue = sourceProperty.GetValue(patchModel);
            targetProperty.SetValue(patchModel, sourceValue);
          }
          else
          {
            throw new ArgumentException($"Invalid source property {value} for copy operation.", nameof(value));
          }
          break;

        case "move":
          var sourceProp = GetPropertyInHierarchy(targetType, value, ignoreCase: true);

          if (sourceProp != null)
          {
            var sourceValue = sourceProp.GetValue(patchModel);
            targetProperty.SetValue(patchModel, sourceValue);
            sourceProp.SetValue(patchModel, null);
          }
          else
          {
            throw new ArgumentException($"Invalid source property {value} for move operation.", nameof(value));
          }
          break;

        case "test":
          var expectedValue = Convert.ChangeType(value, targetProperty.PropertyType);
          var currentValue = targetProperty.GetValue(patchModel);

          if (!Equals(currentValue, expectedValue))
          {
            throw new ArgumentException($"Test failed for property {path}.", nameof(path));
          }
          break;

        default:
          throw new ArgumentException($"Invalid operation {op}.", nameof(op));
      }
    }
    else
    {
      throw new ArgumentException($"Invalid property {path}", nameof(path));
    }
  }

  private PropertyInfo GetPropertyInHierarchy(Type targetType, string propertyName, bool ignoreCase)
  {
    // Search for the property in the inheritance hierarchy
    var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
    if (ignoreCase)
    {
      bindingFlags |= BindingFlags.IgnoreCase;
    }

    var property = targetType.GetProperty(propertyName, bindingFlags);
    if (property == null && targetType.BaseType != null)
    {
      return GetPropertyInHierarchy(targetType.BaseType, propertyName, ignoreCase);
    }
    return property!;
  }
}

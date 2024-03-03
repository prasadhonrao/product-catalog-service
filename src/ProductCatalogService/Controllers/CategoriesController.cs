using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using ProductCatalogService.Entities;
using ProductCatalogService.Models;
using System.Reflection;


namespace ProductCatalogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
  private readonly ProductCatalogServiceContext context;

  public CategoriesController(ProductCatalogServiceContext context)
  {
    this.context = context;
  }

  // GET: api/Categories
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
  {
    return await context.Category.ToListAsync();
  }

  // GET: api/Categories/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Category>> GetCategory(Guid id)
  {
    var category = await context.Category.FindAsync(id);

    if (category == null)
    {
      return NotFound();
    }

    return category;
  }

  // PUT: api/Categories/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPut("{id}")]
  public async Task<IActionResult> PutCategory(Guid id, CategoryModel categoryModel)
  {
    // Check if the Category Guid is valid
    ValidateId(id);

    // Check if category exists in the database
    var category = await context.Category.FindAsync(id);
    if (category == null)
    {
      return NotFound();
    }

    // Check if the model is valid
    if (categoryModel == null)
    {
      return BadRequest();
    }

    if (!ModelState.IsValid)
    {
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
    // Check if the model is valid
    if (categoryModel == null)
    {
      return BadRequest();
    }

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    // Map the model to the entity
    var category = new Category
    {
      Name = categoryModel.Name,
      Description = categoryModel.Description
    };

    try
    {
      context.Category.Add(category);
      await context.SaveChangesAsync();
    }
    catch (DbUpdateException)
    {
      if (CategoryExists(category.Id))
      {
        return Conflict();
      }
      else
      {
        throw;
      }
    }

    return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
  }

  // PATCH: api/Categories/5
  [HttpPatch("{id}")]
  public async Task<IActionResult> PatchCategory(Guid id, JsonPatchDocument<CategoryModel> patchDocument)
  {
    try
    {
      // Check if the Category Guid is valid
      ValidateId(id);

      // Check if category exists in the database
      var category = await context.Category.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }

      // Check if the model is valid
      if (patchDocument == null)
      {
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var patchModel = new CategoryModel
      {
        Name = category.Name,
        Description = category.Description
      };

      // Apply the patch operations to the patchModel
      patchDocument.ApplyTo(patchModel, error =>
      {
        ModelState.AddModelError("", error.ErrorMessage);
      });

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (!TryValidateModel(patchModel))
      {
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
    catch 
    {
      return StatusCode(500, "An error occurred while patching the category. Please try again later.");
    }
  }

  // DELETE: api/Categories/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCategory(Guid id)
  {
    var category = await context.Category.FindAsync(id);
    if (category == null)
    {
      return NotFound();
    }

    try
    {
      context.Category.Remove(category);
      await context.SaveChangesAsync();
    }
    catch (DbUpdateException)
    {
      throw;
    }

    return NoContent();
  }

  private bool CategoryExists(Guid id)
  {
    return context.Category.Any(e => e.Id == id);
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

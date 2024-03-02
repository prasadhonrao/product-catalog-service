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
  public async Task<ActionResult<Category>> GetCategory(int id)
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
  public async Task<IActionResult> PutCategory(int id, Category category)
  {
    if (id != category.Id)
    {
      return BadRequest();
    }

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
  public async Task<ActionResult<Category>> PostCategory(Category category)
  {
    context.Category.Add(category);
    await context.SaveChangesAsync();

    return CreatedAtAction("GetCategory", new { id = category.Id }, category);
  }

  // DELETE: api/Categories/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCategory(int id)
  {
    var category = await context.Category.FindAsync(id);
    if (category == null)
    {
      return NotFound();
    }

    context.Category.Remove(category);
    await context.SaveChangesAsync();

    return NoContent();
  }

  private bool CategoryExists(int id)
  {
    return context.Category.Any(e => e.Id == id);
  }
}

using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDatabaseAdapter _databaseAdapter;

        public CategoriesController(IDatabaseAdapter databaseAdapter)
        {
            _databaseAdapter = databaseAdapter;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _databaseAdapter.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync(string id)
        {
            var category = await _databaseAdapter.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            var result = await _databaseAdapter.CreateCategoryAsync(category);
            if (result == TransactionResult.Success)
            {
                return Ok(category);
            }
            return BadRequest();
        }
    }
}

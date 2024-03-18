using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IDatabaseAdapter databaseAdapter;

    public ProductsController(IDatabaseAdapter databaseAdapter)
    {
        this.databaseAdapter = databaseAdapter;
    }

    [HttpGet(Name = "GetAllProducts")]
    public async Task<ActionResult<List<Product>>> GetAllProductsAsync()
    {
        return await databaseAdapter.GetProductsAsync();
    }
}

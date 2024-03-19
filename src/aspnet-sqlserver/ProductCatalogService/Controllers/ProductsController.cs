using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Data;
using ProductCatalogService.Entities;
using ProductCatalogService.Models;

namespace ProductCatalogService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ProductCatalogServiceContext _context;

    public ProductsController(ProductCatalogServiceContext context)
    {
      _context = context;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts([FromQuery] bool includeRelatedProducts = false)
    {
      var productsQuery = _context.Products.AsQueryable();

      if (includeRelatedProducts)
      {
        productsQuery = productsQuery.Include(p => p.RelatedProducts);
      }

      var products = await productsQuery.ToListAsync();

      var productModels = products.Select(p => new ProductModel
      {
        Id = p.Id,
        ProductName = p.ProductName,
        ProductDescription = p.ProductDescription,
        Price = p.Price,
        Quantity = p.Quantity,
        RelatedProducts = includeRelatedProducts ? p.RelatedProducts.Select(rp => new BasicProductModel
        {
          Id = rp.RelatedProduct.Id,
          ProductName = rp.RelatedProduct.ProductName,
          ProductDescription = rp.RelatedProduct.ProductDescription,
          Price = rp.RelatedProduct.Price,
          Quantity = rp.RelatedProduct.Quantity
        }).ToList() : null
      });

      return Ok(productModels);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
      var product = await _context.Products.FindAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      return product;
    }

    // PUT: api/Products/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(Guid id, Product product)
    {
      if (id != product.Id)
      {
        return BadRequest();
      }

      _context.Entry(product).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProductExists(id))
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

    // POST: api/Products
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }

      _context.Products.Remove(product);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ProductExists(Guid id)
    {
      return _context.Products.Any(e => e.Id == id);
    }
  }
}

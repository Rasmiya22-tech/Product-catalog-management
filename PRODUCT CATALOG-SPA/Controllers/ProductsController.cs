using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRODUCT_CATALOG_SPA.Data;
using PRODUCT_CATALOG_SPA.DTOs;
using PRODUCT_CATALOG_SPA.Models;


namespace PRODUCT_CATALOG_SPA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet("view")]
    public async Task<ActionResult<IEnumerable<object>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();

        var result = products.Select(p => new
        {
            p.Id,
            p.ProductName,
            p.Price,
            p.Description,
            p.Quantity,
            p.Category,
            ImageBase64 = p.ImageData != null
                ? $"data:{p.ImageType};base64,{Convert.ToBase64String(p.ImageData)}"
                : null
        });

        return Ok(result);
    }

    
    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDto dto) // note [FromForm]
    {
        // Duplicate check
        if (await _context.Products.AnyAsync(p => p.ProductName == dto.ProductName))
        {
            return BadRequest(new { message = "Product already exists." });
        }

        var product = new Product
        {
            ProductName = dto.ProductName,
            Price = dto.Price,
            Description = dto.Description,
            Quantity = dto.Quantity,
            Category = dto.Category
        };

        // Handle image upload
        if (dto.Image != null && dto.Image.Length > 0)
        {
            using var ms = new MemoryStream();
            await dto.Image.CopyToAsync(ms);
            product.ImageData = ms.ToArray();      // store bytes
            product.ImageType = dto.Image.ContentType; 
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }

}

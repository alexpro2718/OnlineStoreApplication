using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.DTOs;
using OnlineStore.Models.Entities;

namespace OnlineStore.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    // Список товаров вместо базы данных
    private static List<Product> products = new();

    // ID следующего товара
    private static int nextId = 1;


    // GET: api/products
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = new List<ProductDto>();

        foreach (var product in products)
        {
            result.Add(ToDto(product));
        }

        return Ok(result);
    }


    // GET: api/products/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = products.Find(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(ToDto(product));
    }


    // POST: api/products
    [HttpPost]
    public IActionResult Create(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = nextId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity
        };

        products.Add(product);
        nextId++;

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            ToDto(product));
    }


    // PUT: api/products/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        var product = products.Find(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;

        return NoContent();
    }


    // DELETE: api/products/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = products.Find(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        products.Remove(product);

        return NoContent();
    }


    // Преобразование Entity в DTO
    private ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };
    }
}

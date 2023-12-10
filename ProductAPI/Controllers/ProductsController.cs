using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.Domain;
using ProductAPI.Models.DTO;
using ProductAPI.Repositories.Interface;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await productRepository.GetAllProductsAsync();
        return Ok(products);
    }
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductRequestDto requestDto)
    {
        // Validation logic
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var product = new Product
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                Price = requestDto.Price,
                Image = requestDto.Image
            };

            
            await productRepository.CreateProductAsync(product);

            var response = new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Log the exception (consider using a logging framework like Serilog or NLog)
            Console.Error.WriteLine($"An unexpected error occurred: {ex}");

            // Return an appropriate error response
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid Id)
    {
        var product = await productRepository.GetProductByIdAsync(Id);

        if (product == null)
        {
            return NotFound(); 
        }

        return Ok(product);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<ProductDto>> DeleteByIdAsync(Guid Id)
    {
        var deletedProduct = await productRepository.DeleteProductAsync(Id);

        if (deletedProduct == null)
        {
            return NotFound(); 
        }

        return Ok(deletedProduct);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(Guid id, [FromBody] ProductRequestDto updatedProductDto)
    {
        

        var existingProduct = await productRepository.GetProductByIdAsync(id);

        if (existingProduct == null)
        {
            return NotFound();
        }

       
        existingProduct.Name = updatedProductDto.Name;
        existingProduct.Description = updatedProductDto.Description;

        await productRepository.UpdateProductAsync(id, existingProduct);

        return Ok(existingProduct);
    }
}

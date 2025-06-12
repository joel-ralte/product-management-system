using Microsoft.AspNetCore.Mvc;
using PMS.Business.Services.Interfaces;
using PMS.DTO.Product;

namespace PMS.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing products in the Product Management System (PMS).
    /// </summary>
    /// <param name="productService">The product service instance.</param>
    [ApiController]
    [Route("api/products")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of <see cref="ProductDto"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProducts();
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>
        /// The <see cref="ProductDto"/> if found; otherwise, a 404 Not Found response.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductById(id);
            return product == null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product data to add.</param>
        /// <returns>
        /// A 201 Created response with the created <see cref="ProductDto"/>, 
        /// or a 400 Bad Request if the input is invalid.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequestDto product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newProduct = await productService.AddProduct(product);

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>
        /// A 204 No Content response if the update is successful,
        /// or a 400 Bad Request if the input is invalid,
        /// or a 404 Not Found if the product does not exist.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequestDto product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = await productService.GetProductById(id);

            if (existingProduct == null)
                return NotFound();

            await productService.UpdateProduct(id, product);

            return NoContent();
        }

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>
        /// A 204 No Content response if the deletion is successful,
        /// or a 404 Not Found if the product does not exist.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.GetProductById(id);

            if (product == null)
                return NotFound();
            await productService.DeleteProduct(id);

            return NoContent();
        }

        /// <summary>
        /// Decrements the stock of a product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="quantity">The quantity to decrement.</param>
        /// <returns>
        /// A 200 OK response if the stock is decremented successfully.
        /// </returns>
        [HttpPut("{id}/decrement-stock/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            await productService.DecrementStock(id, quantity);
            return Ok();
        }

        /// <summary>
        /// Adds to the stock of a product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="quantity">The quantity to add.</param>
        /// <returns>
        /// A 200 OK response if the stock is updated successfully.
        /// </returns>
        [HttpPut("{id}/add-to-stock/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            await productService.AddToStock(id, quantity);
            return Ok();
        }

        /// <summary>
        /// Searches for products by name.
        /// </summary>
        /// <param name="name">The name or partial name to search for.</param>
        /// <returns>
        /// A collection of matching <see cref="ProductDto"/>.
        /// </returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductsByName([FromQuery] string name)
        {
            var products = await productService.SearchProductsByName(name);
            return Ok(products);
        }

        /// <summary>
        /// Retrieves products within a specified stock level range.
        /// </summary>
        /// <param name="min">The minimum stock level.</param>
        /// <param name="max">The maximum stock level.</param>
        /// <returns>
        /// A collection of <see cref="ProductDto"/> within the specified stock range.
        /// </returns>
        [HttpGet("stock-level")]
        public async Task<IActionResult> GetProductsByStockLevel([FromQuery] int min, [FromQuery] int max)
        {
            var products = await productService.GetProductsByStockLevel(min, max);
            return Ok(products);
        }
    }
}

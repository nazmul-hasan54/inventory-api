using AutoMapper;
using InventoryApi.DTOs;
using InventoryApi.Entities;
using InventoryApi.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _repo.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(products);
        }


        /// <summary>
        /// Get product by Id
        /// </summary
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }


        /// <summary>
        /// Create a new product
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _repo.AddAsync(product);
            var createdProductDto = _mapper.Map<ProductDto>(createdProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProductDto.Id }, createdProductDto);
        }


        /// <summary>
        /// Update an existing product
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest();
            var existingProduct = await _repo.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound();
            //var productToUpdate = _mapper.Map<Product>(productDto);
            _mapper.Map(productDto, existingProduct);
            await _repo.UpdateAsync(existingProduct);
            return NoContent();
        }


        /// <summary>
        /// Delete a product
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _repo.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound();
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}

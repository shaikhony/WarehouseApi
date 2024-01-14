using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml;
using WarehouseApi.Dtos;
using WarehouseApi.Helper;
using WarehouseApi.Services;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var products = await _productService.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductDto dto)
        {
            try
            {
                var product = new Product
                {
                    ProductName = dto.ProductName,
                    Minimum = dto.Minimum,
                    QuantityAvailble = dto.QuantityAvailble,
                    Price = dto.Price,
                    Effective = dto.Effective,
                    RelationProGrops = dto.GroupsIds.Select(g => new RelationProGrop
                    {
                        GroupId = g
                    }).ToList()
                };
                await _productService.Add(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id , ProductDto dto)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product == null) return NotFound($"No product was found with ID {id}");
                 _productService.DeleteProductRelations(product);
                product.ProductName = dto.ProductName;
                product.Minimum = dto.Minimum;
                product.QuantityAvailble = dto.QuantityAvailble;
                product.Price = dto.Price;
                product.Effective = dto.Effective;
                product.RelationProGrops = dto.GroupsIds.Select(g => new RelationProGrop
                {
                    GroupId = g
                }).ToList();
                _productService.Update(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("EffectiveUpdate {id}")]
        public async Task<IActionResult> EffectiveUpdate(int id , ProductEffectiveDto dto)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product == null) return NotFound($"No product was found with ID {id}");

                product.Effective = dto.Effective;
                _productService.Update(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product == null) return NotFound($"No product was found with ID {id}");
                _productService.Delete(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

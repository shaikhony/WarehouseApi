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
        private readonly ApplicationDbContext _context;

        public ProductsController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
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
        [HttpGet("EffectiveProducts")]
        public async Task<IActionResult> EffectiveProducts()
        {
            try
            {
                var products = await _productService.GetEffectiveProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("MinimumProducts")]
        public async Task<IActionResult> MinimumProducts()
        {
            try
            {
                var products = await _productService.GetMinimumProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Top 10")]
        public IActionResult Tpo10()
        {
            var query = from m in _context.Products
                        join e in _context.Exporteds on m.Id equals e.ProductId into mj
                        from sube in mj.DefaultIfEmpty()
                        orderby sube.Quantity descending
                        select new
                        {
                            ProductName = m.ProductName,
                            Quantity = sube.Quantity
                        };

            var result = query.Take(10).ToList();
            return Ok(result);
        }
        [HttpGet("GetProductSummary")]
        public async Task<IEnumerable<ProductSum>> GetProductSummary()
        {
            var query = from product in _context.Products
                        join exported in _context.Exporteds on product.Id equals exported.ProductId into exportedGroup
                        from exported in exportedGroup.DefaultIfEmpty()
                        join imported in _context.Importeds on product.Id equals imported.ProductId into importedGroup
                        from imported in importedGroup.DefaultIfEmpty()
                        group new { exported, imported } by new { product.Id, product.ProductName } into grouped
                        select new ProductSum
                        {
                            Id = grouped.Key.Id,
                            ProductName = grouped.Key.ProductName,
                            TotalExported = grouped.Sum(x => x.exported.Quantity),
                            TotalImported = grouped.Sum(x => x.imported.Quantity),
                            TotalOverall = grouped.Sum(x => x.exported.Quantity + x.imported.Quantity)
                        };

            return await query.ToListAsync();
        }
        [HttpGet("GetProductsSummaryLastMonth")]
    public async Task<IEnumerable<ProductSum>> GetProductsSummaryLastMonth()
    {
            var query = from product in _context.Products
                        join exported in _context.Exporteds on product.Id equals exported.ProductId into exportedGroup
                        from exported in exportedGroup.DefaultIfEmpty()
                        join imported in _context.Importeds on product.Id equals imported.ProductId into importedGroup
                        from imported in importedGroup.DefaultIfEmpty()
                        where exported != null && imported != null && exported.Date.Year == DateTime.Now.Year && exported.Date.Month == DateTime.Now.Month -1
                        group new { exported, imported } by new { product.Id, product.ProductName } into grouped
                        select new ProductSum
                        {
                            Id = grouped.Key.Id,
                            ProductName = grouped.Key.ProductName,
                            TotalExported = grouped.Sum(x => x.exported.Quantity),
                            TotalImported = grouped.Sum(x => x.imported.Quantity),
                            TotalOverall = grouped.Sum(x => x.exported.Quantity + x.imported.Quantity)
                        };

            return await query.ToListAsync();
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

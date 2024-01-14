using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Helper;
using WarehouseApi.Services;

namespace WarehouseApi.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        public ProductsReportsController(ApplicationDbContext context , IProductService productService)
        {
            _context = context;
            _productService = productService;
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
                        || exported.Date.Year == DateTime.Now.Year - 1 && exported.Date.Month == 12
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
    }
}

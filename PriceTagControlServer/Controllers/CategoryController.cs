using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceTagControlServer.ApplictaionContext.Models;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;

namespace PriceTagControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _сategoryController;
        public CategoryController(IProductCategoryRepository categoryRepository)
        {
            _сategoryController = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ProductCategory>> Get(string categoryName)
        {
            try
            {
                var productCategory = await _сategoryController.GetByName(categoryName);
                return Ok(productCategory);
            }
            catch (Exception ex)
            {
                return BadRequest("Category Not Found");
            }    
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(string categoryName)
        {
            ProductCategory productCategory = new ProductCategory()
            {
                Id = Guid.NewGuid(),
                Name = categoryName
            };
            await _сategoryController.Create(productCategory);
            return Ok(productCategory);
        }
    }
}

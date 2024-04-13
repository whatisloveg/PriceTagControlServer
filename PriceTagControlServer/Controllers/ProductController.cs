using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceTagControlServer.ApplictaionContext;
using PriceTagControlServer.ApplictaionContext.Contracts;
using PriceTagControlServer.ApplictaionContext.Models;
using PriceTagControlServer.ApplictaionContext.Repositories;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;

namespace PriceTagControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IShopRepository _shopRepository;

        public ProductController(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IShopRepository shopRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _shopRepository = shopRepository;
        }


        [HttpGet]
        public async Task<List<ProductResponse>> Get()
        {
            var products = await _productRepository.Get();

            var productResponses = products.Select(p => new ProductResponse(
                                              p.Name,
                                             _productCategoryRepository.GetById(p.CategoryId).Result.Name,
                                             _shopRepository.GetById(p.ShopId).Result.Address,
                                              p.Price))
                                           .ToList();
            return productResponses;
        }

        [HttpPost]
        public async Task<Guid> Create(string name, string category, string shopAddress, decimal price)
        {
            var productCategory = await _productCategoryRepository.GetByName(category);
            var shop = await _shopRepository.GetByAddress(shopAddress);

            if (productCategory == null || shop == null)
            {
                return Guid.Empty;
            }

            var newProduct = new Product
            {
                Name = name,
                CategoryId = productCategory.Id,
                ProductCategory = productCategory,
                ShopId = shop.Id,
                Shop = shop,
                Price = price
            };

            await _productRepository.Create(newProduct);

            return newProduct.Id;
        }
    }
}

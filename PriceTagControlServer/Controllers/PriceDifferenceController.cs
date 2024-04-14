using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceTagControlServer.ApplictaionContext.Models;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;
using PriceTagControlServer.Services.Interfeces;
using System.Xml.Linq;

namespace PriceTagControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceDifferenceController : ControllerBase
    {
        private readonly ISenderToAIService _senderToAIService;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IShopRepository _shopRepository;
        public PriceDifferenceController(ISenderToAIService senderToAIService, IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository, IShopRepository shopRepository)
        {
            _senderToAIService = senderToAIService;
            _productCategoryRepository = productCategoryRepository;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<(int Percent, string ProductName)>> Get([FromBody] string base64Img, string shopAddress)
        {
            //вызов сервиса отправки в нейроку
            var productData = await _senderToAIService.GetInfo(base64Img);
            //заглушка
            // вызов сохранения в базу даных продукта после ответа сервера
            var productCategory = await _productCategoryRepository.GetByName(productData.ProductCategory);
            var shop = await _shopRepository.GetByAddress(shopAddress);

            var newProduct = new Product
            {
                Name = productData.ProductName,
                CategoryId = productCategory.Id,
                ProductCategory = productCategory,
                ShopId = shop.Id,
                Shop = shop,
                Price = productData.Price
            };
            await _productRepository.Create(newProduct);


           
            var allProducts = await _productRepository.Get();

            //получение товаров схожих по наванию
            var similarProducts = allProducts
          .Where(p => FuzzySharp.Fuzz.PartialRatio(p.Name, productData.ProductName) >= 70)
          .ToList();

            decimal averagePrice = similarProducts.Average(p => p.Price);

            // вычислим разницу в процентном соотношении относительно цены целевого товара
            decimal priceDifferencePercent = ((averagePrice - productData.Price) / productData.Price) * 100;

            return Ok((priceDifferencePercent, productData.ProductName));
        }
    }
}

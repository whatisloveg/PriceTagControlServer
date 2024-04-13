using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;
using PriceTagControlServer.ApplictaionContext.Models;

namespace PriceTagControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        public ShopController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        [HttpGet]
        public async Task<List<Shop>> Get()
        {
            return await _shopRepository.Get();
        }

        [HttpPost]
        public async Task<Guid> Create(string shopName, string address)
        {
            Shop shop = new Shop()
            {
                Id = Guid.NewGuid(),
                Name = shopName,
                Address = address
            };
            await _shopRepository.Create(shop);

            return shop.Id;
        }

        [HttpDelete]
        public async Task<Guid> Delete(Guid id)
        {
            return await _shopRepository.Delete(id); ;
        }
    }
}

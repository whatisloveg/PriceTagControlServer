using PriceTagControlServer.ApplictaionContext.Models;
using System.Threading.Tasks;

namespace PriceTagControlServer.ApplictaionContext.Repositories.Interfeces
{
    public interface IShopRepository
    {
        Task<Guid> Create(Shop shop);
        Task<Guid> Delete(Guid id);
        Task<Shop> GetByAddress(string shopAddress);
        Task<Shop> GetById(Guid id);
        Task<List<Shop>> Get();
    }
}
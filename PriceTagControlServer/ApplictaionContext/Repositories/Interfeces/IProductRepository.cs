using PriceTagControlServer.ApplictaionContext.Models;

namespace PriceTagControlServer.ApplictaionContext.Repositories.Interfeces
{
    public interface IProductRepository
    {
        Task<Guid> Create(Product product);
        Task<Guid> Delete(Guid productId);
        Task<List<Product>> Get();
        Task<Product> GetById(Guid id);
    }
}
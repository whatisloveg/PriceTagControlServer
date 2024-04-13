using PriceTagControlServer.ApplictaionContext.Models;

namespace PriceTagControlServer.ApplictaionContext.Repositories.Interfeces
{
    public interface IProductCategoryRepository
    {
        Task<Guid> Create(ProductCategory productCategory);
        Task<ProductCategory> GetById(Guid id);
        Task<ProductCategory> GetByName(string categoryName);
    }
}
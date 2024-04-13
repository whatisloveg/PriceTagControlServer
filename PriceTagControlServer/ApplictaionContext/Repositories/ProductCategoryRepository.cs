using Microsoft.EntityFrameworkCore;
using PriceTagControlServer.ApplictaionContext.Models;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;

namespace PriceTagControlServer.ApplictaionContext.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductCategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<ProductCategory> GetById(Guid id)
        {
            var productCategory = await _context.ProductCategories
                                                .Where(c => c.Id == id)
                                                .FirstAsync();
            return productCategory;
        }

        public async Task<ProductCategory> GetByName(string categoryName)
        {
            var productCategory = await _context.ProductCategories
                                                .Where(c => c.Name == categoryName)
                                                .FirstAsync();
            return productCategory;
        }
        public async Task<Guid> Create(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();

            return productCategory.Id;
        }

    }
}

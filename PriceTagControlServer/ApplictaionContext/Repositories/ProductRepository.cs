using Microsoft.EntityFrameworkCore;
using PriceTagControlServer.ApplictaionContext.Models;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;

namespace PriceTagControlServer.ApplictaionContext.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return product;
        }

        public async Task<Guid> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<Guid> Delete(Guid productId)
        {
            await _context.Products
                .Where(p => p.Id == productId)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return productId;
        }
    }
}

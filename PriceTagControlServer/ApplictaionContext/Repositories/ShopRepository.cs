using Microsoft.EntityFrameworkCore;
using PriceTagControlServer.ApplictaionContext.Models;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;

namespace PriceTagControlServer.ApplictaionContext.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ApplicationDbContext _context;
        public ShopRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<List<Shop>> Get()
        {
            return await _context.Shops.ToListAsync();
        }
        public async Task<Shop> GetById(Guid id)
        {
            return await _context.Shops
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<Shop> GetByAddress(string shopAddress)
        {
            return await _context.Shops
                .Where(s => s.Address == shopAddress)
                .FirstOrDefaultAsync();
        }
        public async Task<Guid> Create(Shop shop)
        {
            await _context.Shops.AddAsync(shop);
            await _context.SaveChangesAsync();

            return shop.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Shops
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return id;
        }
    }
}

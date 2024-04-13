using Microsoft.EntityFrameworkCore;
using PriceTagControlServer.ApplictaionContext;
using PriceTagControlServer.ApplictaionContext.Models;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;

namespace PriceTagControlServer.ApplictaionContext.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}

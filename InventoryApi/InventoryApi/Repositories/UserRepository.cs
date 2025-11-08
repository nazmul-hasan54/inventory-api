using InventoryApi.Data;
using InventoryApi.Entities;
using InventoryApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            var newUser = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return newUser.Entity;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

using Concycle.Core.Entities;
using Concycle.Data.Context;
using Concycle.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Concycle.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(Guid id) 
        {
                return await _context.Users
                    .Where(u => u.Id == id)
                    .FirstOrDefaultAsync();  
        }

        public async Task AddUserAsync(User user) => await _context.Users.AddAsync(user);

        public void DeleteUser(User user) => _context.Users.Remove(user);

        public async Task SaveUserAsync() => await _context.SaveChangesAsync();
    }
}

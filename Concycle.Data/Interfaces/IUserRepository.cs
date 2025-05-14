using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concycle.Core.Entities;

namespace Concycle.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task AddUserAsync(User user);
        void DeleteUser(User user);
        Task SaveUserAsync();
    }
}

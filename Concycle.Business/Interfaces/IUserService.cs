using Concycle.Core.Dtos;

namespace Concycle.Business.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto?> GetUserById(Guid id);
        Task<UserDto> CreateUser(CreateUserDto createDto);
        Task<bool> DeleteUser(Guid id);
        Task<UserDto?> UpdateUser(Guid id, UserDto userDto);
    }

}

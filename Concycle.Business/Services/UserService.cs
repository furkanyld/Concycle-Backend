using AutoMapper;
using Concycle.Business.Interfaces;
using Concycle.Core.Dtos;
using Concycle.Core.Entities;
using Concycle.Data.Interfaces;
using Concycle.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Concycle.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUser(CreateUserDto createDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = createDto.Name,
                Email = createDto.Email,
                PasswordHash = createDto.Password,
                Score = 100,
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveUserAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user is null) return false;

            _userRepository.DeleteUser(user);
            await _userRepository.SaveUserAsync();

            return true;
        }

        public async Task<UserDto?> UpdateUser(Guid id, UserDto userDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user is null) return null;

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            await _userRepository.SaveUserAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}

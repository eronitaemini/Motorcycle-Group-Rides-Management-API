using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Motorcycle_Group_Rides_Management_API.Authentications;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

        public UserService(IUserRepository repository, IMapper mapper, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDto> RegisterUserAsync(RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.Username);
            if (userExists != null)
            {
                return null; // User already exists
            }

            var user = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                // Handle registration errors
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> LoginUserAsync(UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, userDto.Password))
            {
                return _mapper.Map<UserDto>(user);
            }

            return null; // User not found or incorrect password
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return null; // User not found
            }

            user.UserName = updateUserDto.Username;
            user.Email = updateUserDto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Handle update errors
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> DeleteUserAsync(string id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return "User not found";
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return "Failed to delete user";
            }

            return "User deleted successfully";
        }
    }
}

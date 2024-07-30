using Motorcycle_Group_Rides_Management_API.Authentications;
using Motorcycle_Group_Rides_Management_API.Dtos;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
        Task<UserDto> LoginUserAsync(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
        Task<string> DeleteUserAsync(string id);
    }
}

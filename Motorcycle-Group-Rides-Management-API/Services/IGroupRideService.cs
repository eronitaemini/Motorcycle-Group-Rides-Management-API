using Motorcycle_Group_Rides_Management_API.Dtos;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public interface IGroupRideService
    {
        Task<GroupRideDto> CreateGroupRideAsync(CreateGroupRideDto createDto);
        Task<IEnumerable<GroupRideDto>> GetAllGroupRidesAsync();
        Task<GroupRideDto> GetGroupRideByIdAsync(Guid id);
        Task<GroupRideDto> UpdateGroupRideAsync(Guid id, CreateGroupRideDto updateDto);
        Task DeleteGroupRideAsync(Guid id);
        Task<string> AddUserToGroupRideAsync(Guid groupId, Guid userId);
        Task<string> RemoveUserFromGroupRideAsync(Guid groupId, Guid userId);
    }
}

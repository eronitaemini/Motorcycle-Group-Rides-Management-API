using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface IGroupRideRepository
    {
        Task<List<GroupRide>> GetAllAsync();
        Task<GroupRide> GetByIdAsync(Guid id);
        Task CreateAsync(GroupRide groupRide);
        Task UpdateAsync(GroupRide groupRide);
        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }
}

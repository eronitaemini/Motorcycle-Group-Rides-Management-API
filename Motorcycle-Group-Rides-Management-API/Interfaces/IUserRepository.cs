using Microsoft.AspNetCore.Identity;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface IUserRepository
    {
        Task<List<IdentityUser>> GetAllAsync();
        Task<IdentityUser> GetByIdAsync(string id);
        Task CreateAsync(IdentityUser user);
        Task UpdateAsync(IdentityUser user);
        Task DeleteAsync(string id);
        Task<bool> SaveChangesAsync();
       
    }
}

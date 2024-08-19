using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface ICompatibilityRepository
    {


        Task<Compatibility> GetByIdAsync(int id);
        Task<IEnumerable<Compatibility>> GetAllAsync();
        Task AddAsync(Compatibility compatibility);
        Task UpdateAsync(Compatibility compatibility);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Compatibility>> GetCompatibilitiesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize);


    }
}

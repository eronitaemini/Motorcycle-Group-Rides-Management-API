using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public interface ICompatibilityService
    {
        Task<CompatibilityLevel> CheckCompatibilityAsync(MotorcycleType motorcycleType, RouteType routeType);
        Task<IEnumerable<MotorcycleType>> GetCompatibleMotorcyclesAsync(RouteType routeType);
        Task<IEnumerable<RouteType>> GetCompatibleRoutesAsync(MotorcycleType motorcycleType);
        public Task<CompatibilityDto> GetByIdAsync(int id);
        Task<CompatibilityDto> AddCompatibilityAsync(CreateCompatibilityDto compatibilityDto);
        Task<CompatibilityDto> UpdateCompatibilityAsync( int CompatibilityId,UpdateCompatibilityDto updateCompatibilityDto);
        public Task DeleteAsync(int id);

        Task<IEnumerable<CompatibilityDto>> GetCompatibilitiesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize);

    }
}

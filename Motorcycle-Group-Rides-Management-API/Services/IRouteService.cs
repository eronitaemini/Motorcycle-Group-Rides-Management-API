using System;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.RoutesDto;

namespace Motorcycle_Group_Rides_Management_API.Services
{
	public interface IRouteService
	{
        public Task<List<ViewRouteDto>> GetAllAsync();
        public Task<ViewRouteDto> GetByIdAsync(Guid id);
        public Task CreateAsync(CreateRouteDto route);
        public Task DeleteAsync(Guid id);
        public Task UpdateAsync(Guid RouteId, UpdateRouteDto route);
        Task<IEnumerable<Routes>> GetRoutesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize);
    }
}


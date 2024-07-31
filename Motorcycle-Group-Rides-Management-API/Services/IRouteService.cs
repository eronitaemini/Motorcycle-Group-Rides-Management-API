using System;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Services
{
	public interface IRouteService
	{
        public Task<List<ViewRouteDto>> GetAllAsync();
        public Task<ViewRouteDto> GetByIdAsync(Guid id);
        public Task CreateAsync(CreateRouteDto route);
        public Task DeleteAsync(Guid id);
        public Task UpdateAsync(Guid RouteId, UpdateRouteDto route);
    }
}


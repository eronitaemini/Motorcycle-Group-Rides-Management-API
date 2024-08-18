using System;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
	public interface IRouteRepository
	{

        public Task<List<Routes>> GetAllAsync();
        public Task<Routes> GetByIdAsync(Guid id);
        public Task CreateAsync(Routes route);
        public Task DeleteAsync(Guid id);
        public Task UpdateAsync(Routes route);
        public Task SaveChangesAsync();

    }
}



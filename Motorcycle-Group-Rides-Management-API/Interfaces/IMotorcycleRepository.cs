using System;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
	public interface IMotorcycleRepository
	{
		public Task<List<Motorcycle>> GetAllAsync();
		public Task<Motorcycle> GetByIdAsync(int id);
		public Task CreateAsync(Motorcycle motorcycle);
		public Task DeleteAsync(int id);
		public Task UpdateAsync(Motorcycle motorcycle);
		public Task SaveChangesAsync();
        Task<IEnumerable<Motorcycle>> GetMotorcyclesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize);

    }
}


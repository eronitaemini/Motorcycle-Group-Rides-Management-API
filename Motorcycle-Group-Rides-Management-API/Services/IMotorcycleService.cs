using System;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.MotorcycleDtos;

namespace Motorcycle_Group_Rides_Management_API.Services
{
	public interface IMotorcycleService
	{
        public Task<List<ViewMotorcycleDto>> GetAllAsync();
        public Task<ViewMotorcycleDto> GetByIdAsync(int id);
        public Task CreateAsync(CreateMotorcycleDto motorcycleDto);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id, UpdateMotorcycleDto updateMotorcycleDto);
        Task<IEnumerable<Motorcycle>> GetMotorcyclesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize);
        Task<List<MotorcycleDtos.ViewMotorcycleDto>> GetMotorcyclesByUserIdAsync(Guid userId);

    }
}


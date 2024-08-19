using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.MotorcycleDtos;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private GroupRidesContext _context;
        public MotorcycleRepository(GroupRidesContext motorcycleContext)
		{
            _context = motorcycleContext;
        }

        public async Task CreateAsync(Motorcycle motorcycle)
        {
            await _context.Motorcycles.AddAsync(motorcycle);
        }


        public async Task DeleteAsync(int id)
        {
            var motorcycle = await _context.Motorcycles.FindAsync(id);
            _context.Motorcycles.Remove(motorcycle);
        }

     
        public async Task<List<Motorcycle>> GetAllAsync()
        {
            return await _context.Motorcycles.ToListAsync();
        }


        public async Task<Motorcycle> GetByIdAsync(int id)
        {
            return await _context.Motorcycles.FindAsync(id);
        }

        public async Task<IEnumerable<Motorcycle>> GetMotorcyclesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize)
        {
            var query = _context.Motorcycles.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(m => m.Brand.Contains(searchQuery) || m.Model.Contains(searchQuery));
            }

            // Sorting
            query = ascending
                ? query.OrderBy(m => EF.Property<object>(m, sortBy))
                : query.OrderByDescending(m => EF.Property<object>(m, sortBy));

            // Paging
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Motorcycle>> GetMotorcyclesByUserIdAsync(Guid userId)
        {
            var user = await _context.Users.Include(u => u.Motorcycles)
                                        .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return (user.Motorcycles).ToList();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       

        public async Task UpdateAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Update(motorcycle);
            await _context.SaveChangesAsync();
        }
       

    }
}


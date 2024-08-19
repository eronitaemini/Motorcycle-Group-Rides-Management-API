using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class CompatibilityRepository : ICompatibilityRepository
    {
      
        private GroupRidesContext _context;

        public CompatibilityRepository(GroupRidesContext context)
        {
            
            this._context = context;
            
        }
        public async Task AddAsync(Compatibility compatibility)
        {
            await _context.Compatibilities.AddAsync(compatibility);
        }

        public async Task DeleteAsync(int id)
        {
            var compatibility = await _context.Compatibilities.FindAsync(id);
            _context.Compatibilities.Remove(compatibility);
        }

        public async Task<IEnumerable<Compatibility>> GetAllAsync()
        {
            return await _context.Compatibilities.ToListAsync();
        }

        public async Task<Compatibility> GetByIdAsync(int id)
        {
            return await _context.Compatibilities.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task UpdateAsync(Compatibility compatibility)
        {
            _context.Compatibilities.Update(compatibility);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Compatibility>> GetCompatibilitiesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize)
        {
            var query = _context.Compatibilities.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(c => c.MotorcycleType.ToString().Contains(searchQuery) || c.RouteType.ToString().Contains(searchQuery));
            }

            query = ascending
                ? query.OrderBy(c => EF.Property<object>(c, sortBy))
                : query.OrderByDescending(c => EF.Property<object>(c, sortBy));

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
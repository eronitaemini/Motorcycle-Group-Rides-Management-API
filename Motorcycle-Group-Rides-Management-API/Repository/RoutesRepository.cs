using System;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class RoutesRepository : IRouteRepository
	{
        public GroupRidesContext _context;
        public RoutesRepository(GroupRidesContext context)
		{
            _context = context;
		}

        public async Task CreateAsync(Routes route)
        {
            await _context.Routes.AddAsync(route);
        }

        
        public async Task DeleteAsync(Guid id)
        {
            var route= await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);

        }

        public async Task<List<Routes>> GetAllAsync()
        {
           return await _context.Routes.ToListAsync();
        }

        
        public async Task<Routes> GetByIdAsync(Guid id)
        {
            return await _context.Routes.FindAsync(id);
        }

        public async Task<IEnumerable<Routes>> GetRoutesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize)
        {
            var query = _context.Routes.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(r => r.StartingPoint.Contains(searchQuery) || r.EndingPoint.Contains(searchQuery) || r.Description.Contains(searchQuery));
            }

            // Sorting
            query = ascending
                ? query.OrderBy(r => EF.Property<object>(r, sortBy))
                : query.OrderByDescending(r => EF.Property<object>(r, sortBy));

            // Paging
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
        //check if this is the correct impelementation one more time
        public async Task UpdateAsync(Routes route)
        {
            _context.Routes.Update(route);
            await _context.SaveChangesAsync();
        }

   
    }
}


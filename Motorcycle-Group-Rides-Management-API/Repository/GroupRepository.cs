using System;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class GroupRepository : IGroupRepository
	{

        private GroupRidesContext _context;

        public GroupRepository(GroupRidesContext context) {
            _context = context;
        }


        public async Task CreateAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public async Task DeleteAsync(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(group);
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _context.Groups.ToListAsync();

        }
        public async Task<Group> GetByIdAsync(Guid id)
        {
            return await _context.Groups.FindAsync(id);

        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize)
        {
            var query = _context.Groups.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(g => g.Name.Contains(searchQuery) || g.Description.Contains(searchQuery));
            }

            // Sorting
            query = ascending
                ? query.OrderBy(g => EF.Property<object>(g, sortBy))
                : query.OrderByDescending(g => EF.Property<object>(g, sortBy));

            // Paging
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }

    }
}


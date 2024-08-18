using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Umbraco.Core.Persistence.Repositories;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class GroupRideRepository: IGroupRideRepository
    {
        private readonly GroupRidesContext _context;

        public GroupRideRepository(GroupRidesContext context)
        {
            _context = context;
        }

        public async Task<List<GroupRide>> GetAllAsync()
        {
            return await _context.GroupRides
                .Include(gr => gr.UserGroupRides)
                .ThenInclude(ug => ug.User)
                .ToListAsync();
        }

        public async Task<GroupRide> GetByIdAsync(Guid id)
        {
            return await _context.GroupRides
                .Include(gr => gr.UserGroupRides)
                .ThenInclude(ug => ug.User)
                .FirstOrDefaultAsync(gr => gr.Id == id);
        }

        public async Task CreateAsync(GroupRide groupRide)
        {
            _context.GroupRides.Add(groupRide);
        }

        public async Task UpdateAsync(GroupRide groupRide)
        {
            _context.GroupRides.Update(groupRide);
        }

        public async Task DeleteAsync(Guid id)
        {
            var groupRide = await _context.GroupRides.FindAsync(id);
            if (groupRide != null)
            {
                _context.GroupRides.Remove(groupRide);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}

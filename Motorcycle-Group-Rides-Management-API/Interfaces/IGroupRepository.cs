using System;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
	public interface IGroupRepository
	{
        public Task<List<Group>> GetAllAsync();
        public Task<Group> GetByIdAsync(Guid id);
        public Task CreateAsync(Group group);
        public Task DeleteAsync(Guid id);
        public Task UpdateAsync(Group group);
        public Task SaveChangesAsync();
    }
}


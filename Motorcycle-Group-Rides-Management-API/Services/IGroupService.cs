using System;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.GroupDto;

namespace Motorcycle_Group_Rides_Management_API.Services
{
	public interface IGroupService
	{
        public Task<List<ViewGroupDto>> GetAllAsync();
        public Task<ViewGroupDto> GetByIdAsync(Guid id);
        public Task CreateAsync(CreateUpdateGroupDto createGroupDto);
        public Task DeleteAsync(Guid id);
        public Task UpdateAsync(Guid RouteId, CreateUpdateGroupDto updateGroupDto);
        Task<IEnumerable<Group>> GetGroupsAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize);


    }
}


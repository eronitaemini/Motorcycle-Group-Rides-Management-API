using System;
using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Repository;
using static Motorcycle_Group_Rides_Management_API.Dtos.GroupDto;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public class GroupService : IGroupService
    {
		private readonly IGroupRepository _repo;
        private readonly IMapper _mapper;
		public GroupService(IGroupRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

        public async Task CreateAsync(GroupDto.CreateUpdateGroupDto createGroupDto)
        {
            createGroupDto.GroupID = new Guid();
            var group = _mapper.Map<Group>(createGroupDto);
            await _repo.CreateAsync(group);
            await _repo.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var selectedGroup = await _repo.GetByIdAsync(id);

            if (selectedGroup == null)
            {

                throw new KeyNotFoundException("Group not found");

            }

            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();

        }

        public async Task<List<GroupDto.ViewGroupDto>> GetAllAsync()
        {
            var allGroups = await _repo.GetAllAsync();
            return _mapper.Map<List<ViewGroupDto>>(allGroups);
        }

        public async Task<GroupDto.ViewGroupDto> GetByIdAsync(Guid id)
        {
            var group = await _repo.GetByIdAsync(id);

            if (group != null)
            {
                return _mapper.Map<ViewGroupDto>(group);
            }
            return null;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize)
        {
            return await _repo.GetGroupsAsync(searchQuery, sortBy, ascending, pageNumber, pageSize);
        }

        public async Task UpdateAsync(Guid GroupId, GroupDto.CreateUpdateGroupDto updateGroupDto)
        {
            var group = await _repo.GetByIdAsync(GroupId);

            if (GroupId != updateGroupDto.GroupID)
            {
                throw new ArgumentException("ID Mismatch");
            }

            if (group == null)
            {
                throw new KeyNotFoundException("Route not found");
            }

            _mapper.Map(updateGroupDto, group);
            await _repo.UpdateAsync(group);
            await _repo.SaveChangesAsync();
        }
    }
}


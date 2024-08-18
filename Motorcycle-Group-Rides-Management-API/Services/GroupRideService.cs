using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public class GroupRideService : IGroupRideService
    {
        private readonly IGroupRideRepository _repository;
        private readonly IMapper _mapper;

        public GroupRideService(IGroupRideRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GroupRideDto> CreateGroupRideAsync(CreateGroupRideDto createDto)
        {
            var groupRide = _mapper.Map<GroupRide>(createDto);
            await _repository.CreateAsync(groupRide);
            await _repository.SaveChangesAsync();
            return _mapper.Map<GroupRideDto>(groupRide);
        }

        public async Task<IEnumerable<GroupRideDto>> GetAllGroupRidesAsync()
        {
            var groupRides = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GroupRideDto>>(groupRides);
        }

        public async Task<GroupRideDto> GetGroupRideByIdAsync(Guid id)
        {
            var groupRide = await _repository.GetByIdAsync(id);
            return groupRide == null ? null : _mapper.Map<GroupRideDto>(groupRide);
        }

        public async Task<GroupRideDto> UpdateGroupRideAsync(Guid id, CreateGroupRideDto updateDto)
        {
            var groupRide = await _repository.GetByIdAsync(id);
            if (groupRide == null)
            {
                return null;
            }

            _mapper.Map(updateDto, groupRide);
            await _repository.UpdateAsync(groupRide);
            await _repository.SaveChangesAsync();
            return _mapper.Map<GroupRideDto>(groupRide);
        }

        public async Task DeleteGroupRideAsync(Guid id)
        {
            var groupRide = await _repository.GetByIdAsync(id);
            if (groupRide != null)
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<string> AddUserToGroupRideAsync(Guid groupId, Guid userId)
        {
            var groupRide = await _repository.GetByIdAsync(groupId);
            if (groupRide == null)
            {
                return "Group ride not found";
            }

            if (groupRide.UserGroupRides.Any(ug => ug.UserId == userId))
            {
                return "User already in the group ride";
            }

            var userGroupRide = new UserGroupRide
            {
                UserId = userId,
                GroupRideId = groupId
            };

            groupRide.UserGroupRides.Add(userGroupRide);
            await _repository.UpdateAsync(groupRide);
            await _repository.SaveChangesAsync();

            return "User added to group ride successfully";
        }

        public async Task<string> RemoveUserFromGroupRideAsync(Guid groupId, Guid userId)
        {
            var groupRide = await _repository.GetByIdAsync(groupId);
            if (groupRide == null)
            {
                return "Group ride not found";
            }

            var userGroupRide = groupRide.UserGroupRides
                .FirstOrDefault(ug => ug.UserId == userId);

            if (userGroupRide == null)
            {
                return "User not found in group ride";
            }

            groupRide.UserGroupRides.Remove(userGroupRide);
            await _repository.UpdateAsync(groupRide);
            await _repository.SaveChangesAsync();

            return "User removed from group ride successfully";
        }
    }
}

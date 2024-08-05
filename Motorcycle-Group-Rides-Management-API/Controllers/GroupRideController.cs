using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRideController: ControllerBase
    {
        private readonly IGroupRideRepository _repository;
        private readonly IMapper _mapper;

        public GroupRideController(IGroupRideRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("CreateGroupRide")]
        public async Task<IActionResult> CreateGroupRide([FromBody] CreateGroupRideDto createDto)
        {
            var groupRide = _mapper.Map<GroupRide>(createDto);
            await _repository.CreateAsync(groupRide);
            await _repository.SaveChangesAsync();
            return Ok("Group ride created successfully.");
        }

        [HttpGet("GetAllGroupRides")]
        public async Task<IActionResult> GetAllGroupRides()
        {
            var groupRides = await _repository.GetAllAsync();
            var groupRideDtos = _mapper.Map<List<GroupRideDto>>(groupRides);
            return Ok(groupRideDtos);
        }

        [HttpGet("GetGroupRideById/{id}")]
        public async Task<IActionResult> GetGroupRideById(Guid id)
        {
            var groupRide = await _repository.GetByIdAsync(id);
            if (groupRide == null)
            {
                return NotFound();
            }
            var groupRideDto = _mapper.Map<GroupRideDto>(groupRide);
            return Ok(groupRideDto);
        }

        [HttpPut("UpdateGroupRide/{id}")]
        public async Task<IActionResult> UpdateGroupRide(Guid id, [FromBody] CreateGroupRideDto updateDto)
        {
            var groupRide = await _repository.GetByIdAsync(id);
            if (groupRide == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, groupRide);
            await _repository.UpdateAsync(groupRide);
            await _repository.SaveChangesAsync();
            return Ok("Group ride updated successfully.");
        }

        [HttpDelete("DeleteGroupRide/{id}")]
        public async Task<IActionResult> DeleteGroupRide(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return Ok("Group ride deleted successfully.");
        }

        [HttpPost("AddUserToGroupRide/{groupId}/{userId}")]
        public async Task<IActionResult> AddUserToGroupRide(Guid groupId, Guid userId)
        {
            var groupRide = await _repository.GetByIdAsync(groupId);
            if (groupRide == null)
            {
                return NotFound("Group ride not found");
            }

            var userGroupRide = new UserGroupRide
            {
                UserId = userId,
                GroupRideId = groupId
            };

            groupRide.UserGroupRides.Add(userGroupRide);
            await _repository.UpdateAsync(groupRide);
            await _repository.SaveChangesAsync();

            return Ok("User added to group ride successfully.");
        }

        [HttpDelete("RemoveUserFromGroupRide/{groupId}/{userId}")]
        public async Task<IActionResult> RemoveUserFromGroupRide(Guid groupId, Guid userId)
        {
            var groupRide = await _repository.GetByIdAsync(groupId);
            if (groupRide == null)
            {
                return NotFound("Group ride not found");
            }

            var userGroupRide = groupRide.UserGroupRides
                .FirstOrDefault(ug => ug.UserId == userId && ug.GroupRideId == groupId);

            if (userGroupRide == null)
            {
                return NotFound("User not found in group ride");
            }

            groupRide.UserGroupRides.Remove(userGroupRide);
            await _repository.UpdateAsync(groupRide);
            await _repository.SaveChangesAsync();

            return Ok("User removed from group ride successfully.");
        }


    }
}

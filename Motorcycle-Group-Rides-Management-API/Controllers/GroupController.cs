using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Services;
using static Motorcycle_Group_Rides_Management_API.Dtos.GroupDto;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GroupController : ControllerBase
	{
		private IGroupService _groupService;

		public GroupController( IGroupService groupService)
		{
			_groupService = groupService;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllGroups()
		{
            var allGroups = await _groupService.GetAllAsync();
            return Ok(allGroups);

        }

		[HttpGet("{id}")]
		public async Task<ActionResult> GetGroupById(Guid id)
		{
            var group = await _groupService.GetByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

		[HttpPost]
		public async Task<ActionResult> CreateGroup([FromBody] CreateUpdateGroupDto createGroupDto)
		{
            await _groupService.CreateAsync(createGroupDto);
            return new CreatedResult("Location", createGroupDto.Name);
        }

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateGroup(Guid id, [FromBody] CreateUpdateGroupDto updateGroupDto)
		{
            try
            {
                await _groupService.UpdateAsync(id, updateGroupDto);
                return NoContent();

            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException);
                return BadRequest("ID Mismatch");
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                Console.WriteLine(keyNotFoundException);
                return NotFound("Group not found");
            }
        }

		[HttpDelete]
		public async Task<ActionResult> DeleteGroup(Guid id)
		{
            try
            {
                await _groupService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException keyNotFoundExeption)
            {
                Console.WriteLine(keyNotFoundExeption);
                return NotFound("The group was not found and couldn't be deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "An error occurred while deleting the group");
            }

        }

        [HttpGet("search")]
        public async Task<ActionResult> GetGroups(string searchQuery = "", string sortBy = "Name", bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            var groups = await _groupService.GetGroupsAsync(searchQuery, sortBy, ascending, pageNumber, pageSize);
            return Ok(groups);
        }



    }
}


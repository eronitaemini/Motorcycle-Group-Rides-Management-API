using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.GroupDto;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GroupController : ControllerBase
	{
		private IMapper _mapper;
		private IGroupRepository _repo;

		public GroupController(IMapper mapper, IGroupRepository repo)
		{
			_mapper = mapper;
			_repo = repo;
		}

		[HttpGet]
		public ActionResult<List<ViewGroupDto>> GetAllGroups()
		{
			try
			{
				var groups = _repo.GetAll();
				var viewGroupDtoList = _mapper.Map<List<ViewGroupDto>>(groups);

				return Ok(viewGroupDtoList);
			} catch (KeyNotFoundException e)
			{
				Console.WriteLine(e.Message);
				return BadRequest();
			}

		}

		[HttpGet("{id}")]
		public ActionResult<ViewGroupDto> GetGroupById(Guid id)
		{
			var group = _repo.GetById(id);
			if (group != null)
			{
				var viewGroupDto = _mapper.Map<ViewGroupDto>(group);
				return Ok(viewGroupDto);
			}

			return BadRequest();
		}

		[HttpPost]
		public ActionResult CreateGroup([FromBody] CreateGroupDto createGroupDto)
		{
			var group = _mapper.Map<Group>(createGroupDto);
			group.CreationDate = DateTime.Now;
			_repo.Create(group);
			_repo.SaveChanges();
			return new CreatedResult("location", createGroupDto.Name);
		}

		[HttpPut("{id}")]
		public ActionResult UpdateGroup(Guid id, [FromBody] UpdateGroupDto updateGroupDto)
		{
			if (id != updateGroupDto.GroupID)
			{
				return BadRequest();
			}
			
			var group = _mapper.Map<Group>(updateGroupDto);
			_repo.Update(group);
			_repo.SaveChanges();

			return NoContent();
		}

		[HttpDelete]
		public ActionResult DeleteGroup(Guid id)
		{
			var group = _repo.GetById(id);

			if (group != null)
			{
				_repo.Delete(id);
				_repo.SaveChanges();
				return NoContent();
			}
			return BadRequest();

		}
		


    }
}


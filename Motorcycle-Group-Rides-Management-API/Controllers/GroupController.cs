using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using static Motorcycle_Group_Rides_Management_API.GroupDtos;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
	[ApiController]
	[Route("[api/[controller]]")]
	public class GroupController:ControllerBase
	{

		private IMapper _mapper;
		private IGroupRepository _repo;

		public GroupController(IMapper mapper, IGroupRepository repo)
		{
			_mapper = mapper;
			_repo = repo;
		}

		//[HttpGet]
		//public ActionResult<List<ViewGroupDto>> GetAllGroups()
		//{
		//	var groups= Ok(_repo.GetAll());
		//	var viewGroupDto = _mapper.Map<ViewGroupDto>(groups);
		//	return Ok(viewGroupDto);

		//}


	
	}
}


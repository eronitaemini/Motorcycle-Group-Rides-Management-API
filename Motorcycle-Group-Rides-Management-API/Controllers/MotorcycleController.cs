using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.MotorcycleDtos;

//potential proper error handling

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MotorcycleController:ControllerBase
	{
		private IMapper _mapper;
		private IMotorcycleRepository _motorcycleRepository;

		public MotorcycleController(IMapper mapper, IMotorcycleRepository repo)
		{
			_mapper = mapper;
            _motorcycleRepository = repo;
		}

		[HttpGet]
		public ActionResult<List<MotorcycleDto>> GetAllMotorcycles()
		{

			try
			{
				var motorcycles = _motorcycleRepository.GetAll();
				var motorcyclesDto = _mapper.Map<List<MotorcycleDto>>(motorcycles);
				return Ok(motorcyclesDto);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return BadRequest();
			}
		}

		[HttpGet("{id}")]
		public ActionResult<MotorcycleDto> GetMotorcycleById(int id)
		{
			var motorcycle = _motorcycleRepository.GetById(id);

			if (motorcycle != null)
			{
				var motorcycleDto = _mapper.Map<MotorcycleDto>(motorcycle);
				return Ok(motorcycleDto);
			}
		
            return BadRequest();
		}


		[HttpPost]
		public ActionResult CreateMotorcycle([FromBody] CreateMotorcycleDto createMotorcyleDto)
		{
			var motorcycle = _mapper.Map<Motorcycle>(createMotorcyleDto);
			 _motorcycleRepository.Create(motorcycle);
			_motorcycleRepository.SaveChanges();
			return new CreatedResult("location", motorcycle.Brand);
		}

		[HttpDelete]
		public ActionResult DeleteMotorcycle(int motorcycleId)
		{
			var motorcycleToDelete = _motorcycleRepository.GetById(motorcycleId);

			if (motorcycleToDelete != null)
			{
				_motorcycleRepository.Delete(motorcycleId);
				_motorcycleRepository.SaveChanges();
				return NoContent();
			}
			return BadRequest();
		}

		//[HttpPut("{id}")]
		//public ActionResult UpdateMotorcycle(int id, [FromBody] UpdateMotorcycleDto updateMotorcycleDto)
		//{
		//	var exisitingMotorcycle = _motorcycleRepository.GetById(id);
		//	if (exisitingMotorcycle != null)
		//	{
		//		_mapper.Map(updateMotorcycleDto, exisitingMotorcycle);
		//		exisitingMotorcycle.Brand = updateMotorcycleDto.Brand;
		//		exisitingMotorcycle.EngineSize = updateMotorcycleDto.EngineSize;
		//		exisitingMotorcycle.OwnerID = updateMotorcycleDto.OwnerID;
		//		exisitingMotorcycle.Model = updateMotorcycleDto.Model;

		//		return Ok(exisitingMotorcycle);
		//	}

		//	return BadRequest();
		//}

	}
}


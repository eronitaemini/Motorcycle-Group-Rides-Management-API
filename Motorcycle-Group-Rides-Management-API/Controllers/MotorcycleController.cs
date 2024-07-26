
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.MotorcycleDtos;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MotorcycleController:ControllerBase
	{
		private IMapper _mapper;
		private IMotorcycleRepository _repo;

		public MotorcycleController(IMapper mapper, IMotorcycleRepository repo)
		{
			_mapper = mapper;
            _repo = repo;
		}

		[HttpGet]
		public ActionResult<List<ViewMotorcycleDto>> GetAllMotorcycles()
		{

			try
			{
				var motorcycles = _repo.GetAll();
				var motorcyclesDto = _mapper.Map<List<ViewMotorcycleDto>>(motorcycles);
				return Ok(motorcyclesDto);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return BadRequest();
			}
		}

		[HttpGet("{id}")]
		public ActionResult<ViewMotorcycleDto> GetMotorcycleById(int id)
		{
			var motorcycle = _repo.GetById(id);

			if (motorcycle != null)
			{
				var motorcycleDto = _mapper.Map<ViewMotorcycleDto>(motorcycle);
				return Ok(motorcycleDto);
			}
		
            return BadRequest("Motorcycle not found");
		}


		[HttpPost]
		public ActionResult CreateMotorcycle([FromBody] CreateMotorcycleDto createMotorcyleDto)
		{
			var motorcycle = _mapper.Map<Motorcycle>(createMotorcyleDto);
            _repo.Create(motorcycle);
            _repo.SaveChanges();
			return new CreatedResult("location", motorcycle.Brand);
		}

		[HttpDelete]
		public ActionResult DeleteMotorcycle(int motorcycleId)
		{
			var motorcycleToDelete = _repo.GetById(motorcycleId);

			if (motorcycleToDelete != null)
			{
                _repo.Delete(motorcycleId);
                _repo.SaveChanges();
				return NoContent();
			}
			return BadRequest();

		}


		[HttpPut("{id}")]
		public ActionResult UpdateMotorcycle(int id, [FromBody] UpdateMotorcycleDto updateMotorcycleDto)
		{
			if (id != updateMotorcycleDto.MotorcycleID)
			{
				return BadRequest("ID mismatch");
			}

			var existingMotorcycle = _repo.GetById(id);
			if (existingMotorcycle == null)
			{
				return NotFound("Motorcycle not found");
			}

			try
			{
				_mapper.Map(updateMotorcycleDto, existingMotorcycle);

                _repo.Update(existingMotorcycle);
                _repo.SaveChanges();
				return NoContent();

			}catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}


    }
}


using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Services;
using static Motorcycle_Group_Rides_Management_API.Dtos.MotorcycleDtos;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MotorcycleController:ControllerBase
	{
		private IMapper _mapper;
		private readonly IMotorcycleService _motorcycleService;
		public MotorcycleController(IMapper mapper, IMotorcycleService service)
		{
			_mapper = mapper;
            _motorcycleService = service;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllMotorcycles()
		{

            var allMotorcycles = await _motorcycleService.GetAllAsync();
            return Ok(allMotorcycles);
        }

		[HttpGet("{id}")]
		public async Task<ActionResult> GetMotorcycleById(int id)
		{
            var motorcycle = await _motorcycleService.GetByIdAsync(id);

            if (motorcycle == null)
            {
                return NotFound();
            }
            return Ok(motorcycle);
        }


		[HttpPost]
		public async Task<ActionResult> CreateMotorcycle([FromBody] CreateMotorcycleDto createMotorcyleDto)
		{
            await _motorcycleService.CreateAsync(createMotorcyleDto);
            return new CreatedResult("Location", createMotorcyleDto.Brand);
        }

		[HttpDelete]
		public async Task<ActionResult> DeleteMotorcycle(int motorcycleId)
		{
            try
            {
                await _motorcycleService.DeleteAsync(motorcycleId);
                return NoContent();
            }
            catch (KeyNotFoundException keyNotFoundExeption)
            {
                Console.WriteLine(keyNotFoundExeption);
                return NotFound("The motorcycle was not found and couldn't be deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "An error occurred while deleting the route");
            }

        }


		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateMotorcycle(int id, [FromBody] UpdateMotorcycleDto updateMotorcycleDto)
		{
            try
            {
                await _motorcycleService.UpdateAsync(id, updateMotorcycleDto);
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
                return NotFound("Motorcycle not found");
            }

        }


    }
}


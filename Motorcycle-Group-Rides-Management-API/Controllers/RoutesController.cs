using System;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.External;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Services;
using static Motorcycle_Group_Rides_Management_API.Dtos.RoutesDto;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RoutesController : ControllerBase
	{
		private readonly IRouteService _routeService;
		public RoutesController(IRouteService routeService, IRouteInfo routeInfo)
		{
			_routeService = routeService;
			
		}

		[HttpGet]
		public async Task<ActionResult> GetAllRoute()
		{
			var allRoute = await _routeService.GetAllAsync();
			return Ok(allRoute);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetRouteById(Guid id)
		{
			var route = await _routeService.GetByIdAsync(id);

			if (route == null)
			{
				return NotFound();
			}
			return Ok(route);
		}

		[HttpPost]
		public async Task<ActionResult> CreateRoute([FromBody] CreateRouteDto createRouteDto)
		{

			await _routeService.CreateAsync(createRouteDto);
			return new CreatedResult("Location", createRouteDto.Description);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateRoute(Guid id, [FromBody] UpdateRouteDto updateRouteDto)
		{

			try
			{
				await _routeService.UpdateAsync(id, updateRouteDto);
				return NoContent();

			} catch (ArgumentException argumentException)
			{
				Console.WriteLine(argumentException);
				return BadRequest("ID Mismatch");
			}
			catch (KeyNotFoundException keyNotFoundException)
			{
				Console.WriteLine(keyNotFoundException);
				return NotFound("Route not found");
			}


		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteRouteById(Guid id) {

			try
			{
                await _routeService.DeleteAsync(id);
				return NoContent();
            }catch (KeyNotFoundException keyNotFoundExeption)
			{
				Console.WriteLine(keyNotFoundExeption);
				return NotFound("The route was not found and couldn't be deleted");
			}
			catch (Exception e)
			{
                Console.WriteLine(e);
                return StatusCode(500, "An error occurred while deleting the route");
			}

		}

    }
}


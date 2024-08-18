using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompatibilityController : ControllerBase
    {
        private readonly ICompatibilityService _compatibilityService;

        public CompatibilityController(ICompatibilityService compatibilityService)
        {
            _compatibilityService = compatibilityService;
        }

        // GET: api/Compatibility/check
        [HttpGet("check")]
        public async Task<IActionResult> CheckCompatibilityAsync([FromQuery] MotorcycleType motorcycleType, [FromQuery] RouteType routeType)
        {
            try
            {
                var compatibilityLevel = await _compatibilityService.CheckCompatibilityAsync(motorcycleType, routeType);
                return Ok(new { CompatibilityLevel = compatibilityLevel });
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error checking compatibility: {ex.Message}");
            }
        }

        // GET: api/Compatibility/motorcycles
        [HttpGet("motorcycles")]
        public async Task<IActionResult> GetCompatibleMotorcyclesAsync([FromQuery] RouteType routeType)
        {
            try
            {
                var compatibleMotorcycles = await _compatibilityService.GetCompatibleMotorcyclesAsync(routeType);
                return Ok(compatibleMotorcycles);
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving compatible motorcycles: {ex.Message}");
            }
        }

        // GET: api/Compatibility/routes
        [HttpGet("routes")]
        public async Task<IActionResult> GetCompatibleRoutesAsync([FromQuery] MotorcycleType motorcycleType)
        {
            try
            {
                var compatibleRoutes = await _compatibilityService.GetCompatibleRoutesAsync(motorcycleType);
                return Ok(compatibleRoutes);
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving compatible routes: {ex.Message}");
            }
        }

        // POST: api/Compatibility
        [HttpPost]
        public async Task<IActionResult> AddCompatibilityAsync([FromBody] CreateCompatibilityDto compatibilityDto)
        {
            /* if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             try
             {
                 await _compatibilityService.AddCompatibilityAsync(compatibilityDto);
                 return CreatedAtAction(nameof(GetCompatibilityById), new { id = compatibilityDto }, compatibilityDto);
             }
             catch (Exception ex)
             {
                 // Log the exception details here
                 return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating compatibility: {ex.Message}");
             }*/

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdCompatibility = await _compatibilityService.AddCompatibilityAsync(compatibilityDto);
                return CreatedAtAction(nameof(GetCompatibilityById), new { id = createdCompatibility.Id }, createdCompatibility);
            }
            catch (InvalidOperationException ex)
            {
                // Return a conflict response if a duplicate compatibility is detected
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating compatibility: {ex.Message}");
            }

        }


        // PUT: api/Compatibility/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompatibilityAsync(int id, [FromBody] UpdateCompatibilityDto updateCompatibilityDto)
        {
            if (id != updateCompatibilityDto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _compatibilityService.UpdateCompatibilityAsync(id, updateCompatibilityDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating compatibility: {ex.Message}");
            }
        }

        // GET: api/Compatibility/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompatibilityById(int id)
    {
        try
        {
            var compatibility = await _compatibilityService.GetByIdAsync(id);
            if (compatibility == null)
            {
                return NotFound();
            }
            return Ok(compatibility);
        }
        catch (Exception ex)
        {
            // Log the exception details here
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving compatibility: {ex.Message}");
        }
    }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompatibilityDto>>> GetCompatibilities(
       [FromQuery] string searchQuery = "",
       [FromQuery] string sortBy = "MotorcycleType",
       [FromQuery] bool ascending = true,
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10)
        {
            var compatibilities = await _compatibilityService.GetCompatibilitiesAsync(searchQuery, sortBy, ascending, pageNumber, pageSize);
            return Ok(compatibilities);
        }

    }
    }


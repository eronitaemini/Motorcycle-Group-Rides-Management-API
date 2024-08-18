using System;
using Microsoft.AspNetCore.Mvc;

namespace Motorcycle_Group_Rides_Management_API.External
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteInfoController : ControllerBase
    {
        private readonly IRouteInfo _routeInfo;
        public RouteInfoController(IRouteInfo routeInfo)
        {
            _routeInfo = routeInfo;
        }

        [HttpGet("{origin}/{destination}")]
        public async Task<IActionResult> GetRouteInfoAsync(string origin, string destination)
        {
          var routeinfo = await _routeInfo.GetRouteInfoAsync(origin, destination);

            if (routeinfo != null)
            {

                return Ok(routeinfo);
            }
            return BadRequest("error");

        }
        
    }

   
}

using System;
using Motorcycle_Group_Rides_Management_API.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
	public class ViewRouteDto
	{

        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public float Distance { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public RouteType RouteType { get; set; }
        public string Description { get; set; }
    
	}
}


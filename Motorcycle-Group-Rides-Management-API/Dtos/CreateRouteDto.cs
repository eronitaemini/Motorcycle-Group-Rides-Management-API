using System;
using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
	public class CreateRouteDto
	{
		public Guid RouteId { get; set; }
		public string StartingPoint { get; set; }
		public string EndingPoint { get; set; }
        public string Description { get; set; }
    }
}


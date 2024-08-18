using System;
using System.ComponentModel.DataAnnotations;
using Motorcycle_Group_Rides_Management_API.Helpers;

namespace Motorcycle_Group_Rides_Management_API.Models
{
	public class Routes
	{
		[Key]
		public Guid RouteId { get; set; }
		[Required]
		public string StartingPoint { get; set; }
		[Required]
		public string EndingPoint { get; set; }
		public string Distance { get; set; }
		public string EstimatedTime { get; set; }
		public RouteType RouteType { get; set; }
		public string Description { get; set; }

        public int Popularity { get; set; }
        public List<GroupRide> GroupRide { get; set; }
	}
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Motorcycle_Group_Rides_Management_API.Helpers;

namespace Motorcycle_Group_Rides_Management_API.Models
{
    [Table("Routes")]
	public class Route
	{
        [Key]
        public int RouteID { get; set; }
        [Required]
        public string StartPoint { get; set; }
        [Required]
        public string EndPoint { get; set; }
        public string Distance { get; set; }
        public string EstimatedTime { get; set; }
        public string GoogleMapsRouteData { get; set; }
        public string SafetyTips { get; set; }
        public RouteType Type { get; set; }

       // public List<GroupRide> Rides { get; set; }
    }
}


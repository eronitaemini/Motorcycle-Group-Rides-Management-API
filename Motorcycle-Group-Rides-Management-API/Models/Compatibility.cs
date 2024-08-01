using Motorcycle_Group_Rides_Management_API.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Models
{
    public class Compatibility
    {
        [Key]
        public int CompatibilityId { get; set; }

        [Required]
        public MotorcyleType MotorcycleType { get; set; }

        [Required]
        public RouteType RouteType { get; set; }

        [Required]
        public CompatibilityLevel CompatibilityLevel { get; set;}

    }
}
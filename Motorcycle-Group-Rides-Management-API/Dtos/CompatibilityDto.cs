using Motorcycle_Group_Rides_Management_API.Helpers;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class CompatibilityDto
    {
        public int Id { get; set; }
        public MotorcyleType MotorcycleType { get; set; }
        public RouteType RouteType { get; set; }
        public CompatibilityLevel CompatibilityLevel { get; set; }
    }

    public class CheckCompatibilityDto
    {
        public MotorcyleType MotorcycleType { get; set; }
        public RouteType RouteType { get; set; }
    }

}

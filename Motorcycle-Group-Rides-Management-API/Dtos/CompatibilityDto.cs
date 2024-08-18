using Motorcycle_Group_Rides_Management_API.Helpers;
using System.Text.Json.Serialization;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class CompatibilityDto
    {
        public int Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MotorcycleType MotorcycleType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RouteType RouteType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CompatibilityLevel CompatibilityLevel { get; set; }
    }
    public class CreateCompatibilityDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MotorcycleType MotorcycleType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RouteType RouteType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CompatibilityLevel CompatibilityLevel { get; set; }
    }

    public class CheckCompatibilityDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MotorcycleType MotorcycleType { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RouteType RouteType { get; set; }
    }

    public class UpdateCompatibilityDto
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MotorcycleType? MotorcycleType { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RouteType? RouteType { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CompatibilityLevel? CompatibilityLevel { get; set; }
    }

}

using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public interface ICompatibilityService
    {
        // bool IsCompatible(MotorcyleType motorcycleType, RouteType routeType);
        CompatibilityLevel CheckCompatibility(int motorcycleId, int routeId);
     //   IEnumerable<Route> GetAllCompatibleRoutes(int motorcycleId);
        Compatibility UpdateCompatibility(int compatibilityId, CompatibilityDto compatibilityDto);
        bool DeleteCompatibility(int compatibilityId);
        Compatibility GetCompatibilityDetails(int compatibilityId);
    }
}

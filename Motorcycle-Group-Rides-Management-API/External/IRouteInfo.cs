namespace Motorcycle_Group_Rides_Management_API.External
{
    public interface IRouteInfo
    {
        public Task<RouteInfo> GetRouteInfoAsync(string origin, string destination);
    }
}
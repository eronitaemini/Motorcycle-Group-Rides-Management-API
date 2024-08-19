using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Helpers;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<double> GetAverageParticipantsPerRideAsync();
        Task<IEnumerable<PopularityRouteDto>> GetMostPopularRoutesAsync();

    //    Task<Dictionary<RouteType, int>> GetIncidentCountByRouteAsync();


        Task<Dictionary<string, int>> GetIncidentCountByLocationAsync();

      //  Task<Dictionary<Guid, double>> GetAverageFeedbackRatingPerRideAsync();

        public Task<IEnumerable<KeyValuePair<Guid, double>>> GetAverageFeedbackRatingPerRideAsync(
            string searchQuery = null,
            int? minRating = null,
            int? maxRating = null,
            int pageNumber = 1,
            int pageSize = 10);

    }





}

using Motorcycle_Group_Rides_Management_API.Dtos;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public interface IStatisticsService
    {
        Task<double> GetAverageParticipantsPerRideAsync();
        Task<IEnumerable<IncidentLocationCountDto>> GetIncidentCountByLocationAsync();
        Task<IEnumerable<PopularityRouteDto>> GetMostPopularRoutesAsync();

      //  Task<Dictionary<Guid, double>> GetAverageFeedbackRatingPerRideAsync();

        Task<IEnumerable<KeyValuePair<Guid, double>>> GetAverageFeedbackRatingPerRideAsync(
        string searchQuery = null,
        int? minRating = null,
        int? maxRating = null,
        int pageNumber = 1,
        int pageSize = 10);
    }
}

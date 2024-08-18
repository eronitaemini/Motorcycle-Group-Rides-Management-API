using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Repository;
using Umbraco.Core.Persistence;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IMapper _mapper;
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILogger<StatisticsService> _logger;

        public StatisticsService(IMapper mapper, IStatisticsRepository statisticsRepository, ILogger<StatisticsService> logger )
        {

            _mapper = mapper;
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<IncidentLocationCountDto>> GetIncidentCountByLocationAsync()
        {
            var incidentCounts = await _statisticsRepository.GetIncidentCountByLocationAsync();

            return incidentCounts.Select(ic => new IncidentLocationCountDto
            {
                Location = ic.Key,
                IncidentCount = ic.Value
            });
        }

        public async Task<IEnumerable<PopularityRouteDto>> GetMostPopularRoutesAsync()
        {
          //  return await _statisticsRepository.GetMostPopularRoutesAsync();
            var popularRoutes = await _statisticsRepository.GetMostPopularRoutesAsync();
            return _mapper.Map<IEnumerable<PopularityRouteDto>>(popularRoutes);
        }


        public async Task<double> GetAverageParticipantsPerRideAsync()
        {
            
         return await _statisticsRepository.GetAverageParticipantsPerRideAsync();
        }



        /*  public async Task<Dictionary<Guid, double>> GetAverageFeedbackRatingPerRideAsync()
          {
              return await _statisticsRepository.GetAverageFeedbackRatingPerRideAsync();
          }*/


        public async Task<IEnumerable<KeyValuePair<Guid, double>>> GetAverageFeedbackRatingPerRideAsync(
       string searchQuery = null,
       int? minRating = null,
       int? maxRating = null,
       int pageNumber = 1,
       int pageSize = 10)
        {
            _logger.LogInformation("Fetching average feedback ratings with searchQuery: {SearchQuery}, minRating: {MinRating}, maxRating: {MaxRating}, pageNumber: {PageNumber}, pageSize: {PageSize}",
                                    searchQuery, minRating, maxRating, pageNumber, pageSize);

            // Call the repository method
            var result = await _statisticsRepository.GetAverageFeedbackRatingPerRideAsync(searchQuery, minRating, maxRating, pageNumber, pageSize);

            _logger.LogInformation("{RatingCount} average feedback ratings fetched", result.Count());

            return result;
        }

    }
}

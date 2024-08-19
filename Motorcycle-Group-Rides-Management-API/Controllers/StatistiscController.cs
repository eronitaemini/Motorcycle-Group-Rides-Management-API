using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // GET: api/Statistics/IncidentsByLocation
        [HttpGet("IncidentsByLocation")]
        public async Task<ActionResult<IEnumerable<IncidentLocationCountDto>>> GetIncidentsByLocation()
        {
            var incidentCounts = await _statisticsService.GetIncidentCountByLocationAsync();
            return Ok(incidentCounts);
        }

        // GET: api/Statistics/MostPopularRoutes
        [HttpGet("MostPopularRoutes")]
        public async Task<ActionResult<IEnumerable<PopularityRouteDto>>> GetMostPopularRoutes()
        {
            var popularRoutes = await _statisticsService.GetMostPopularRoutesAsync();
            return Ok(popularRoutes);
        }

        // GET: api/Statistics/AverageParticipantsPerRide
        [HttpGet("AverageParticipantsPerRide")]
        public async Task<ActionResult<double>> GetAverageParticipantsPerRide()
        {
            var averageParticipants = await _statisticsService.GetAverageParticipantsPerRideAsync();
            return Ok(averageParticipants);
        }

        // GET: api/Statistics/AverageFeedbackRatingPerRide
      /*  [HttpGet("AverageFeedbackRatingPerRide")]
        public async Task<ActionResult<Dictionary<Guid, double>>> GetAverageFeedbackRatingPerRide()
        {
            var feedbackRatings = await _statisticsService.GetAverageFeedbackRatingPerRideAsync();
            return Ok(feedbackRatings);
        }*/


        [HttpGet("average-feedback-rating")]
        public async Task<ActionResult> GetAverageFeedbackRating(
        string searchQuery = null,
        int? minRating = null,
        int? maxRating = null,
        int pageNumber = 1,
        int pageSize = 10)
        {
            var averageRatings = await _statisticsService.GetAverageFeedbackRatingPerRideAsync(
                searchQuery, minRating, maxRating, pageNumber, pageSize);

            return Ok(averageRatings);
        }


    }

}

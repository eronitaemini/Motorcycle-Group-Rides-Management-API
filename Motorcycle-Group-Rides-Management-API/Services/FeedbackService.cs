using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Umbraco.Core.Persistence;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;
using static Umbraco.Core.Constants;


namespace Motorcycle_Group_Rides_Management_API.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FeedbackService> _logger;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper, ILogger<FeedbackService> logger)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<FeedbackDto.ViewFeedbackDto>> GetAllAsync()
        {
            var allFeedbacks = await _feedbackRepository.GetAllAsync();
            return _mapper.Map<List<ViewFeedbackDto>>(allFeedbacks);
        }

        public async Task<IEnumerable<FeedbackDto.ViewFeedbackDto>> GetFeedbacksByGroupRideIdAsync(Guid groupRideId)
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbackByGroupRidesIdAsync(groupRideId);
            return _mapper.Map<IEnumerable<ViewFeedbackDto>>(feedbacks);
        }

        public async Task<FeedbackDto.ViewFeedbackDto> GetFeedbackByIdAsync(Guid id)
        {
               var feedback = await _feedbackRepository.GetByIdAsync(id);
               return feedback == null ? null : _mapper.Map<ViewFeedbackDto>(feedback);

            //var feedback = await _feedbackRepository.GetByIdAsync(id);

            //if (feedback != null)
            //{
             //   return _mapper.Map<FeedbackDto>(feedback);
            //}
           // return null;
        }

        public async Task<FeedbackDto> CreateFeedbackAsync(FeedbackDto.CreateFeedbackDto feedbackDto)
        {
             var feedback =  _mapper.Map<Feedback>(feedbackDto);

             await _feedbackRepository.CreateAsync(feedback);
             await _feedbackRepository.SaveChangesAsync();
             return _mapper.Map<FeedbackDto>(feedback);

            


        }


        public async Task<FeedbackDto> UpdateFeedbackAsync(Guid FeedbackId, UpdateFeedbackDto updateFeedbackDto)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(FeedbackId);



            if (feedback == null)
            {
                throw new KeyNotFoundException("Route not found");
            }

            _mapper.Map(updateFeedbackDto, feedback);
            await _feedbackRepository.UpdateAsync(feedback);
            await _feedbackRepository.SaveChangesAsync();
            return _mapper.Map<FeedbackDto>(feedback);
        }




        public async Task DeleteFeedbackAsync(Guid id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback != null)
            {
                await _feedbackRepository.DeleteAsync(id);
                await _feedbackRepository.SaveChangesAsync();
            }
        }




        public async Task<IEnumerable<Feedback>> GetFeedbacksAsync(
        string searchQuery,
        string sortBy,
        bool ascending,
        int pageNumber,
        int pageSize,
        int? minRating = null,
        int? maxRating = null)
        {
            _logger.LogInformation("Fetching feedbacks with parameters: " +
                                   "searchQuery='{SearchQuery}', sortBy='{SortBy}', ascending={Ascending}, " +
                                   "pageNumber={PageNumber}, pageSize={PageSize}, minRating={MinRating}, maxRating={MaxRating}",
                                   searchQuery, sortBy, ascending, pageNumber, pageSize, minRating, maxRating);

            try
            {
                var feedbacks = await _feedbackRepository.GetFeedbacksAsync(searchQuery, sortBy, ascending, pageNumber, pageSize, minRating, maxRating);

                _logger.LogInformation("Successfully fetched {FeedbackCount} feedbacks.", feedbacks.Count());

                return feedbacks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching feedbacks with parameters: " +
                                     "searchQuery='{SearchQuery}', sortBy='{SortBy}', ascending={Ascending}, " +
                                     "pageNumber={PageNumber}, pageSize={PageSize}, minRating={MinRating}, maxRating={MaxRating}",
                                     searchQuery, sortBy, ascending, pageNumber, pageSize, minRating, maxRating);

                // Handle the exception (e.g., rethrow, return an empty list, etc.)
                throw;
            }
        }


    }
}
   
              



using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;
using static Motorcycle_Group_Rides_Management_API.Dtos.GroupDto;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public interface IFeedbackService
    {
        public Task<List<FeedbackDto.ViewFeedbackDto>> GetAllAsync();
        Task<IEnumerable<FeedbackDto.ViewFeedbackDto>> GetFeedbacksByGroupRideIdAsync(Guid groupRideId);
        Task<FeedbackDto.ViewFeedbackDto> GetFeedbackByIdAsync(Guid id);
        Task<FeedbackDto> CreateFeedbackAsync(CreateFeedbackDto feedbackDto);
        Task<FeedbackDto> UpdateFeedbackAsync(Guid FeedbackId, UpdateFeedbackDto updateFeedbackDto);
        Task DeleteFeedbackAsync(Guid id);

        Task<IEnumerable<Feedback>> GetFeedbacksAsync(
                    string searchQuery,
                    string sortBy,
                    bool ascending,
                    int pageNumber,
                    int pageSize,
                    int? minRating = null,
                    int? maxRating = null);

    }
}

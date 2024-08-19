using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface IFeedbackRepository
    {
        public Task<List<Feedback>> GetAllAsync();
        public  Task <IEnumerable<Feedback>> GetAllFeedbackByGroupRidesIdAsync(Guid grouprideId);
        public Task<Feedback> GetByIdAsync(Guid id);
        public Task CreateAsync(Feedback feedback);
        public Task DeleteAsync(Guid id);
        public Task UpdateAsync(Feedback feedback);
        public Task<bool> SaveChangesAsync();
        // Pagination and Filtering
        //    Task<List<Feedback>> GetFeedbacksAsync(Guid? groupRideId, int pageNumber, int pageSize, int? rating);

        public Task<IEnumerable<Feedback>> GetFeedbacksAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize, int? minRating = null, int? maxRating = null);
    }
}

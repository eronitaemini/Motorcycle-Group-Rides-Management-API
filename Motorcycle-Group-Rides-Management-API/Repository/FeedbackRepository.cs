using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {

        private GroupRidesContext _context;
        public FeedbackRepository(GroupRidesContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);




        }

        public async Task DeleteAsync(Guid id)
        {
            var selectedfeedback = await _context.Feedbacks.FindAsync(id);
            _context.Feedbacks.Remove(selectedfeedback);
        }

        //public List<Feedback> GetAll()
        //{
        //  return _context.Feedbacks.ToList();
        //}
        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _context.Feedbacks.ToListAsync();

        }
        public async Task<IEnumerable<Feedback>> GetAllFeedbackByGroupRidesIdAsync(Guid grouprideId)
        {
            return _context.Feedbacks.Include(x => x.GroupRide).Where(x => x.GroupRideId == grouprideId);
        }
        public async Task<Feedback> GetByIdAsync(Guid id)
        {
            //   return await _context.Feedbacks
            //  .Include(x => x.GroupRide)
            //   .FirstOrDefaultAsync(x => x.GroupRideId == id);

            return await _context.Feedbacks.FirstOrDefaultAsync(f => f.FeedbackId == id);

            //   return  await _context.Feedbacks.FindAsync(id);



        }



        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task UpdateAsync(Feedback feedback)
        {
            _context.Update(feedback);
            await _context.SaveChangesAsync();
        }

        /*    public async Task<List<Feedback>> GetFeedbacksAsync(Guid? groupRideId, int pageNumber, int pageSize, int? rating)
            {
                var query = _context.Feedbacks.AsQueryable();

                if (groupRideId.HasValue)
                {
                    query = query.Where(f => f.GroupRideId == groupRideId.Value);
                }

                if (rating.HasValue)
                {
                    query = query.Where(f => f.Rating == rating.Value);
                }

                return await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();


            }
        */
        public async Task<IEnumerable<Feedback>> GetFeedbacksAsync( string searchQuery, string sortBy,bool ascending,int pageNumber,  int pageSize,  int? minRating = null, int? maxRating = null)
        {
            var query = _context.Feedbacks.AsQueryable();

            // Filtering by search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Handle GUID search query
                if (Guid.TryParse(searchQuery, out Guid userId))
                {
                    query = query.Where(m => m.UserId == userId);
                }
                else
                {
                    query = query.Where(m => m.Comments.Contains(searchQuery));
                }
            }

            // Filtering by rating range
            if (minRating.HasValue)
            {
                query = query.Where(m => m.Rating >= minRating.Value);
            }
            if (maxRating.HasValue)
            {
                query = query.Where(m => m.Rating <= maxRating.Value);
            }

            // Sorting
            query = ascending
                ? query.OrderBy(m => EF.Property<object>(m, sortBy))
                : query.OrderByDescending(m => EF.Property<object>(m, sortBy));

            // Paging
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }



    }
}
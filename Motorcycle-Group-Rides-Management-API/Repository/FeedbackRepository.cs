using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private GroupRidesContext _context;
        public FeedbackRepository(GroupRidesContext context) 
        {
              this._context = context;
        }

        public void Create(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
        }

        public void Delete(Guid id)
        {
            var selectedfeedback = _context.Feedbacks.Find(id);
            _context.Feedbacks.Remove(selectedfeedback);
        }

        //public List<Feedback> GetAll()
        //{
        //  return _context.Feedbacks.ToList();
        //}
        public IEnumerable<Feedback> GetAllFeedbackByGroupRidesId(Guid grouprideId)
        {
            return _context.Feedbacks.Include(x => x.GroupRide).Where(x => x.GroupRideId == grouprideId);
        }
        public Feedback GetById(Guid id)
        {
            return _context.Feedbacks.Include(x => x.GroupRide).FirstOrDefault(x => x.GroupRideId == id);

            // return _context.Feedbacks.Find(id);


        }



        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        public void Update(Feedback feedback)
        {
            _context.Update(feedback);
        }
    }
}

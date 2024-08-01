using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface IFeedbackRepository
    {
       // public List<Feedback> GetAll();
        public  IEnumerable<Feedback> GetAllFeedbackByGroupRidesId(Guid grouprideId);
        public Feedback GetById(Guid id);
        public void Create(Feedback feedback);
        public void Delete(Guid id);
        public void Update(Feedback feedback);
        public bool SaveChanges();
    }
}

using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class FeedbackDto
    {

       

        public class ViewFeedbackDto {
            //+
            public Guid FeedbackId { get; set; }
            public string Comments { get; set; }
            public int Rating { get; set; }
            public DateTime DateSubmitted { get; set; } = DateTime.Now;
        }
        public class CreateFeedbackDto 
        {
        //    public Guid FeedbackId { get; set; }
            public string Comments { get; set; }
            public int Rating { get; set; }
            public DateTime DateSubmitted { get; set; } = DateTime.Now;

            public Guid UserId { get; set; }  // Foreign key reference to User

            public Guid GroupRideId { get; set; }
        }
        public class UpdateFeedbackDto 
        {
            public Guid FeedbackId { get; set; }
            public string Comments { get; set; }
            public int Rating { get; set; }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class FeedbackDto
    {

        public class ViewFeedbackDto {
           
            public string Comments { get; set; }
            public int Rating { get; set; }
            public DateTime DateSubmitted { get; set; } = DateTime.Now;
        }
        public class CreateFeedbackDto 
        {
            public int FeedbackId { get; set; }
            public string Comments { get; set; }
            public int Rating { get; set; }
            public DateTime DateSubmitted { get; set; } = DateTime.Now;
        }
        public class UpdateFeedbackDto 
        {
            public int FeedbackId { get; set; }
            public string Comments { get; set; }
            public int Rating { get; set; }
        }
    }
}

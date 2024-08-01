using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motorcycle_Group_Rides_Management_API.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        public Guid FeedbackId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comments { get; set; }
        public int Rating { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey(nameof(User))]
        public User Author { get; set; }
        public Guid userId { get; set; }
        [Required]
        public Guid GroupRideId { get; set; }
        public virtual GroupRide GroupRide { get; set; }
    }
}

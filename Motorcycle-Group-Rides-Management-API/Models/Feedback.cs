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

        // Foreign key for the User
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }  // Foreign key column

        public virtual User User { get; set; }  // Navigation property

        // Foreign key for the GroupRide
        [Required]
        [ForeignKey(nameof(GroupRide))]
        public Guid GroupRideId { get; set; }

        public virtual GroupRide GroupRide { get; set; }
    }


}

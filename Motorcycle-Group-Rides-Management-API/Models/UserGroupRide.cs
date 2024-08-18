using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motorcycle_Group_Rides_Management_API.Models
{
    public class UserGroupRide
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey(nameof(GroupRide))]
        public Guid GroupRideId { get; set; }
        public GroupRide GroupRide { get; set; }
    }
}

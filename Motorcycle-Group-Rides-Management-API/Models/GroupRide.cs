using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motorcycle_Group_Rides_Management_API.Models
{
        [Table("GroupRides")]
        public class GroupRide
        {
                [Key]
                public Guid Id { get; set; } = Guid.NewGuid();

                [Required]
                [MaxLength(100)]
                public string Name { get; set; }

                [Required]
                public string Description { get; set; }

                [Required]
                [ForeignKey(nameof(Route))]
                public Guid RouteID { get; set; }
                public Routes Route { get; set; }
                public List<User> Participants { get; set; }
                public string StartPoint { get; set; }

                [Required]
                public string EndPoint { get; set; }

                public ICollection<UserGroupRide> UserGroupRides { get; set; } = new List<UserGroupRide>();

        [Required]
        public List<Feedback> Feedbacks { get; set; }

      //  public bool Compatible { get; set; }


        }
}

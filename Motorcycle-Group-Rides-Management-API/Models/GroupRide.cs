﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motorcycle_Group_Rides_Management_API.Models
{
    [Table("GroupRides")]
    public class GroupRide
    {
        [Key]
        public Guid GroupRideId { get; set; }

        [Required]
        public Guid GroupID { get; set; }
        public Group Group { get; set; }

        [Required]
        public Guid OrganizerId { get; set; }
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Route))]
        public Guid RouteID { get; set; }
        public Routes Route { get; set; }
        public List<User> Participants { get; set; }



    }
}
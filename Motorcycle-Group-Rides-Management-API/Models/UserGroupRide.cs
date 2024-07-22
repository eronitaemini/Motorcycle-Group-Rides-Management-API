using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Motorcycle_Group_Rides_Management_API.Models
{
    [Table("UserGroupRide")]
    [PrimaryKey(nameof(UserID), nameof(GroupRideID))]
    public class UserGroupRide
    {
        public Guid UserID { get; set; }
        public Guid GroupRideID { get; set; }

        public User User { get; set; }
        public GroupRide GroupRide { get; set; }
    }
}


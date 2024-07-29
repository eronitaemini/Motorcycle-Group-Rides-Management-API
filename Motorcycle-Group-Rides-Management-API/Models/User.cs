using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motorcycle_Group_Rides_Management_API.Models
{
    [Table("Users")]
    public class User
    {
        internal readonly object UserGroupRides;

        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public List<Motorcycle> Motorcycles { get; set; }
        public ICollection<UserGroupRide> UserGroupRide { get; set; } = new List<UserGroupRide>();







    }
}


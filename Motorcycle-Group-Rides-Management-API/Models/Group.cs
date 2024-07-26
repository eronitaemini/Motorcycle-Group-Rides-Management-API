using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motorcycle_Group_Rides_Management_API.Models
{
    [Table("Groups")]
	public class Group
	{
		
        [Key]
        public Guid GroupID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public List<GroupRide> GroupRides { get; set; } 
    
	}
}


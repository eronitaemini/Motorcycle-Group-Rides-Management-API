using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class UpdateIncidentStatusDto
    {
        [Required]
        public string Status { get; set; }
    }
}

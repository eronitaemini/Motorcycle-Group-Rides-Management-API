using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class CreateIncidentReportDto
    {
        public Guid Id { get; set; }

        [Required]
        public string RideId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string IncidentType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int Severity { get; set; }
    }
}

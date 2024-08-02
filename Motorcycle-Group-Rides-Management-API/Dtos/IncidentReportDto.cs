using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class IncidentReportDto
    {
        public Guid Id { get; set; }

     
        public string UserId { get; set; }

     
        public string IncidentType { get; set; }

      
        public string Description { get; set; }

     
        public string Location { get; set; }

       
        public DateTime Time { get; set; }

    
        public int Severity { get; set; }

        public string Status { get; set; }
    }
}

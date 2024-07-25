namespace Motorcycle_Group_Rides_Management_API.Models
{
    public class IncidentReport
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string RideId { get; set; }
        public string UserId { get; set; }
        public string IncidentType { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public int Severity { get; set; }
        public string Status { get; set; } = "pending";
    }
}

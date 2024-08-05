namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class GroupRideDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}

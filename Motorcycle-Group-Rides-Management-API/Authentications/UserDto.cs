namespace Motorcycle_Group_Rides_Management_API.Authentications
{
    public class UserDto
    {
       public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       public List<Guid> GroupRideIds { get; set; }
    }
}

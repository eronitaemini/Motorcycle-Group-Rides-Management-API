using System.ComponentModel.DataAnnotations;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}

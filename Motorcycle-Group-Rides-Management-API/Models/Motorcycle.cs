using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Motorcycle_Group_Rides_Management_API.Helpers;


	namespace Motorcycle_Group_Rides_Management_API.Models
{
	[Table("Motorcycles")]
	public class Motorcycle
	{
		[Key]
		public int MotorcycleID { get; set; }
		[Required]
		public string  Brand { get; set; }
		[Required]
        public string Model { get; set; }
        public int ProductionYear { get; set; }
		[Required]
        public MotorcyleType Type { get; set; }
		[Required]
		public int  EngineSize { get; set; }
		[Required]
		public Guid OwnerID { get; set; }
		public User Owner { get; set; }

	}
}


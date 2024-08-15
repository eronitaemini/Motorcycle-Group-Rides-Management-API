using System;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
	public class GroupDto
	{
		public class ViewGroupDto
		{
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime CreationDate { get; set; }
        }
        
        public class CreateUpdateGroupDto
        {
            public Guid GroupID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
    
        }

	}
}


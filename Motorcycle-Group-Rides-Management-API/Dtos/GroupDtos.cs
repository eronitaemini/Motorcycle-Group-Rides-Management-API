using System;
using System.ComponentModel.DataAnnotations;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API
{
	public class GroupDtos
	{
		public class ViewGroupDto
		{
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime CreationDate { get; set; }
            public List<GroupRide> GroupRides { get; set; }
        }

        public class CreateGroupDto
        {
            public Guid GroupID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime CreationDate { get; set; }

            CreateGroupDto()
            {
                GroupID = Guid.NewGuid();
            }

        }

        public class UpdateGroupDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public List<GroupRide> GroupRides { get; set; }

        }


    }
}


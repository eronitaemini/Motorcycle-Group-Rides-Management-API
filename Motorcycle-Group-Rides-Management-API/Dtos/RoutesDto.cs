using System;
using Motorcycle_Group_Rides_Management_API.Helpers;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
	public class RoutesDto
	{
        public class ViewRouteDto
        {

            public string StartingPoint { get; set; }
            public string EndingPoint { get; set; }
            public string Distance { get; set; }
            public string EstimatedTime { get; set; }
            public RouteType RouteType { get; set; }
            public string Description { get; set; }

        }

        public class CreateRouteDto
        {
            public Guid RouteId { get; set; }
            public string StartingPoint { get; set; }
            public string EndingPoint { get; set; }
            public string Description { get; set; }
        }
        public class UpdateRouteDto
        {
            public Guid RouteId { get; set; }

            public string StartingPoint { get; set; }
            public string EndingPoint { get; set; }
            public string Description { get; set; }

        }

        


    }
}


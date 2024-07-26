using System;
using Motorcycle_Group_Rides_Management_API.Helpers;

namespace Motorcycle_Group_Rides_Management_API.Dtos
{
    public class MotorcycleDtos
    {

        public class ViewMotorcycleDto
        {
            public int MotorcycleID { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public int ProductionYear { get; set; }
            public MotorcyleType Type { get; set; }
        }

        public class CreateMotorcycleDto
        {
            public string Brand { get; set; }
            public string Model { get; set; }
            public MotorcyleType Type { get; set; }
            public int EngineSize { get; set; }
            public Guid OwnerID { get; set; }
            public int ProductionYear { get; set; }
        }

        public class UpdateMotorcycleDto
        {
            public int MotorcycleID { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public MotorcyleType Type { get; set; }
            public int EngineSize { get; set; }
        }
    }
}


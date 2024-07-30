using System;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
	public interface IRouteRepository
	{
		
            public List<Routes> GetAll();
            public Routes GetById(Guid routeId);
            public void Create(Routes route);
            public void Delete(Guid id);
            public void Update(Routes route);
            public bool SaveChanges();
        
    }
}



using System;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
	public interface IGroupRepository
	{
        public List<Group> GetAll();
        public Group GetById(Guid id);
        public void Create(Group group);
        public void Delete(Guid id);
        public void Update(Group group);
        public bool SaveChanges();
    }
}


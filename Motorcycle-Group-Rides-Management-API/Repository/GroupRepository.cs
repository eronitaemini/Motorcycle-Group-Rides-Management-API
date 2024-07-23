using System;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
	public class GroupRepository:IGroupRepository
	{
        private GroupRidesContext _context;
		public GroupRepository(GroupRidesContext context)
		{
            _context = context;
		}

        public void Create(Group group)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAll()
        {
            return _context.Groups.ToList();
        }

        public Group GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}


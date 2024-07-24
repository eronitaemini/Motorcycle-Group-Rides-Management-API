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
            _context.Groups.Add(group);
        }

        public void Delete(Guid id)
        {
            var group = _context.Groups.Find(id);
            _context.Groups.Remove(group);
        }

        public List<Group> GetAll()
        {
            return _context.Groups.ToList();
        }

        public Group GetById(Guid id)
        {
            return _context.Groups.Find(id);
            
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        public void Update(Group group)
        {
            _context.Groups.Update(group);
        }
    }
}


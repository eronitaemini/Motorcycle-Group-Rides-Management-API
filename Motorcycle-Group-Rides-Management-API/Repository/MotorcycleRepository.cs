using System;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
	public class MotorcycleRepository:IMotorcycleRepository
	{
        private GroupRidesContext _context;
        public MotorcycleRepository(GroupRidesContext motorcycleContext)
		{
            _context = motorcycleContext;
        }

        public void Create(Motorcycle motorcycle)
        {
            _context.Motorcycles.Add(motorcycle);
        }

        public void Delete(int id)
        {
            var selectedMotorcycle = _context.Motorcycles.Find(id);
            _context.Motorcycles.Remove(selectedMotorcycle);
        }

        public List<Motorcycle> GetAll()
        {
            return _context.Motorcycles.ToList();
        }

        public Motorcycle GetById(int id)
        {
            return _context.Motorcycles.Find(id);
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        public void Update(Motorcycle motorcycle)
        {
            _context.Motorcycles.Update(motorcycle);
        }
    }
}


using System;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private GroupRidesContext _motorcycleContext;

        public MotorcycleRepository(GroupRidesContext motorcycleContext)
        {
            _motorcycleContext = motorcycleContext;
        }

        public void Create(Motorcycle motorcycle)
        {
            _motorcycleContext.Motorcycles.Add(motorcycle);

        }

        public void Delete(int id)
        {
            var selectedMotorcycle = _motorcycleContext.Motorcycles.Find(id);
            _motorcycleContext.Motorcycles.Remove(selectedMotorcycle);
        }

        public List<Motorcycle> GetAll()
        {
            return _motorcycleContext.Motorcycles.ToList();

        }

        public Motorcycle GetById(int id)
        {
            return _motorcycleContext.Motorcycles.Find(id);
        }

        public bool SaveChanges()
        {
            _motorcycleContext.SaveChanges();
            return true;
        }

        public void Update(Motorcycle motorcycle)
        {
            throw new NotImplementedException();
        }


        //public void Update(Motorcycle motorcycle)
        //{

        //    //var existingmotorcycle=Get
        //    _motorcycleContext.Motorcycles.Update(motorcycle);

        //}
    
    }
}


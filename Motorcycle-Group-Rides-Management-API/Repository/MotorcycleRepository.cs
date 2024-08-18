using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private GroupRidesContext _context;
        public MotorcycleRepository(GroupRidesContext motorcycleContext)
		{
            _context = motorcycleContext;
        }

        public async Task CreateAsync(Motorcycle motorcycle)
        {
            await _context.Motorcycles.AddAsync(motorcycle);
        }


        public async Task DeleteAsync(int id)
        {
            var motorcycle = await _context.Motorcycles.FindAsync(id);
            _context.Motorcycles.Remove(motorcycle);
        }

     
        public async Task<List<Motorcycle>> GetAllAsync()
        {
            return await _context.Motorcycles.ToListAsync();
        }


        public async Task<Motorcycle> GetByIdAsync(int id)
        {
            return await _context.Motorcycles.FindAsync(id);
        }

    
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       

        public async Task UpdateAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Update(motorcycle);
            await _context.SaveChangesAsync();
        }

       
    }
}


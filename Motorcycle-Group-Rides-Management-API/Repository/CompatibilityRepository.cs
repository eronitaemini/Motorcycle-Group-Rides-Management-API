using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class CompatibilityRepository : ICompatibilityRepository
    {
        private GroupRidesContext _context;
        public CompatibilityRepository(GroupRidesContext context)
        {
            _context = context;
        }

        public Compatibility GetCompatibilityById(int compatibilityId)
        {
            return _context.Compatibilities.Find(compatibilityId);
        }

        public void AddCompatibility(Compatibility compatibility)
        {
            _context.Compatibilities.Add(compatibility);
        }

        public void UpdateCompatibility(Compatibility compatibility)
        {
            _context.Compatibilities.Update(compatibility);
        }

        public void DeleteCompatibility(Compatibility compatibility)
        {
            _context.Compatibilities.Remove(compatibility);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
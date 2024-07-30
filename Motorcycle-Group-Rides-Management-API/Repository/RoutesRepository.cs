using System;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class RoutesRepository : IRouteRepository
	{
        public GroupRidesContext _context;
        public RoutesRepository(GroupRidesContext context)
		{
            _context = context;
		}

        public void Create(Routes route)
        {
            _context.Routes.Add(route);
        }

        public void Delete(Guid id)
        {
            var selectedRoute = _context.Routes.Find(id);
            _context.Routes.Remove(selectedRoute);
        }

        public List<Routes> GetAll()
        {
            return _context.Routes.ToList();
        }

        public Routes GetById(Guid routeId)
        {
            var selectedRoute = _context.Routes.Find(routeId);
            return selectedRoute;


        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        public void Update(Routes route)
        {
            _context.Routes.Update(route);

        }

        //public async Task Create(Routes route)
        //{
        //   await _context.Routes.AddAsync(route);
        //   await _context.SaveChangesAsync();
        //}

        //public async Task Delete(Guid id)
        //{

        //    //IDk
        //     var selectedRoute=await _context.Routes.FindAsync(id);
        //     _context.Remove(selectedRoute);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<List<Routes>> GetAll()
        //{
        //    return await _context.Routes.ToListAsync();
        //}

        //public async Task<Routes> GetById(Guid routeId)
        //{
        //    return await _context.Routes.FindAsync(routeId);
        //}

        //public bool SaveChanges()
        //{
        //    throw new NotImplementedException();
        //}

        ////public bool SaveChanges()
        ////{
        ////    _context.SaveChanges();
        ////    return true;
        ////}

        //public async Task Update(Routes route)
        //{
        //    _context.Attach(route);
        //    _context.Entry(route).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}


    }
}


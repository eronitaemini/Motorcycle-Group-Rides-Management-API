using System;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.PopularityRouteDto;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        public GroupRidesContext _context;


        public StatisticsRepository(GroupRidesContext context)
        {
            _context = context;
        }
        public async Task<double> GetAverageParticipantsPerRideAsync()
        {
            /*  return await _context.GroupRides
                  .Select(gr => gr.Participants.Count)  // Assuming Participants is a collection of users
                  .DefaultIfEmpty(0)
                  .AverageAsync();*/
            // Load the data into memory and then calculate the average
            var groupRides = await _context.GroupRides
                .Include(gr => gr.Participants) // Ensure that Participants are included
                .ToListAsync();

            // Calculate the average participants per ride
            var averageParticipants = groupRides
                .Select(gr => gr.Participants.Count)
                .DefaultIfEmpty(0)
                .Average();

            return averageParticipants;

        }

        public async Task<Dictionary<string, int>> GetIncidentCountByLocationAsync()
        {
            return await _context.IncidentReports
            .GroupBy(ir => ir.Location)
            .Select(group => new
            {
                Location = group.Key,
                Count = group.Count()
            })
            .ToDictionaryAsync(g => g.Location, g => g.Count);
        }

        /*  public Task<Dictionary<RouteType, int>> GetIncidentCountByRouteAsync()
          {
              throw new NotImplementedException();
          }*/

       

        public async Task<IEnumerable<PopularityRouteDto>> GetMostPopularRoutesAsync()
        {
            return await _context.GroupRides
                .GroupBy(gr => gr.RouteID)  // Assuming there's a RouteId in GroupRide
                .Select(group => new PopularityRouteDto
                {
                    RouteId = group.Key,
                    Popularity = group.Count() // Number of rides using this route
                })
                .OrderByDescending(dto => dto.Popularity)
                .ToListAsync();
        }

      /*  public async Task<Dictionary<Guid, double>> GetAverageFeedbackRatingPerRideAsync()
        {
            return await _context.Feedbacks
                .GroupBy(fb => fb.GroupRideId) // Group by ride ID
                .Select(g => new
                {
                    RideId = g.Key,
                    AverageRating = g.Average(fb => fb.Rating) // Assuming there's a Rating property
                })
                .ToDictionaryAsync(x => x.RideId, x => x.AverageRating);
        }
      */

        public async Task<IEnumerable<KeyValuePair<Guid, double>>> GetAverageFeedbackRatingPerRideAsync(
    string searchQuery = null, 
    int? minRating = null, 
    int? maxRating = null, 
    int pageNumber = 1, 
    int pageSize = 10)
{
    var query = _context.Feedbacks.AsQueryable();

    // Filtering by searchQuery (for example, based on comments or other fields)
    if (!string.IsNullOrEmpty(searchQuery))
    {
        query = query.Where(f => f.Comments.Contains(searchQuery));
    }

    // Filtering by rating range
    if (minRating.HasValue)
    {
        query = query.Where(f => f.Rating >= minRating.Value);
    }
    if (maxRating.HasValue)
    {
        query = query.Where(f => f.Rating <= maxRating.Value);
    }

    // Grouping by GroupRideId and calculating average ratings
    var groupedQuery = query
        .GroupBy(f => f.GroupRideId)
        .Select(g => new 
        { 
            RideId = g.Key, 
            AverageRating = g.Average(f => f.Rating) 
        });

    // Applying pagination
    var pagedResult = await groupedQuery
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return pagedResult.Select(gr => new KeyValuePair<Guid, double>(gr.RideId, gr.AverageRating));
}





    }
}

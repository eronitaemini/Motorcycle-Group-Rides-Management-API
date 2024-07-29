using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class IncidentReportRepository : IIncidentReportRepository
    {
        private readonly GroupRidesContext _context;

        public IncidentReportRepository(GroupRidesContext context)
        {
            _context = context;
        }

        public async Task<List<IncidentReport>> GetAllAsync(int pageNumber, int pageSize, int? severity = null, string location = null)
        {
            var query = _context.IncidentReports.AsQueryable();

            if (severity.HasValue)
            {
                query = query.Where(ir => ir.Severity == severity.Value);
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(ir => EF.Functions.Like(ir.Location, $"%{location}%"));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<IncidentReport> GetByIdAsync(Guid id)
        {
            return await _context.IncidentReports.FindAsync(id);
        }

        public async Task CreateAsync(IncidentReport incidentReport)
        {
            _context.IncidentReports.Add(incidentReport);
        }

        public async Task UpdateAsync(IncidentReport incidentReport)
        {
            _context.IncidentReports.Update(incidentReport);
        }

        public async Task DeleteAsync(Guid id)
        {
            var incidentReport = await _context.IncidentReports.FindAsync(id);
            if (incidentReport != null)
            {
                _context.IncidentReports.Remove(incidentReport);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }


}

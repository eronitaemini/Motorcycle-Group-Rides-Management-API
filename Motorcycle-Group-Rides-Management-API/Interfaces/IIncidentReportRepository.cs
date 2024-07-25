using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface IIncidentReportRepository
    {
        Task<List<IncidentReport>> GetAllAsync();
        Task<IncidentReport> GetByIdAsync(Guid id);
        Task CreateAsync(IncidentReport incidentReport);
        Task UpdateAsync(IncidentReport incidentReport);
        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }
}

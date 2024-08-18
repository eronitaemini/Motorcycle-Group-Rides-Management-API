using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface IIncidentReportRepository
    {
        Task<List<IncidentReport>> GetAllAsync(int pageNumber, int pageSize, int? severity = null, string location = null);
        Task<IncidentReport> GetByIdAsync(Guid id);
        Task CreateAsync(IncidentReport incidentReport);
        Task UpdateAsync(IncidentReport incidentReport);
        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }


}

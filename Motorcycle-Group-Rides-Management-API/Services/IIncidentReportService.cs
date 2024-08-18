using Motorcycle_Group_Rides_Management_API.Dtos;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public interface IIncidentReportService
    {
        Task<IncidentReportDto> GetIncidentReportByIdAsync(Guid id);
        Task<IEnumerable<IncidentReportDto>> GetAllIncidentReportsAsync(int pageNumber, int pageSize, int? severity = null, string location = null);
        Task<IncidentReportDto> CreateIncidentReportAsync(CreateIncidentReportDto createDto);
        Task<IncidentReportDto> UpdateIncidentStatusAsync(Guid id, UpdateIncidentStatusDto updateDto);
        Task DeleteIncidentReportAsync(Guid id);
    }
}

using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public class IncidentReportService: IIncidentReportService
    {
        private readonly IIncidentReportRepository _repository;
        private readonly IMapper _mapper;

        public IncidentReportService(IIncidentReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IncidentReportDto> GetIncidentReportByIdAsync(Guid id)
        {
            var incidentReport = await _repository.GetByIdAsync(id);
            if (incidentReport == null)
            {
                return null;
            }
            return _mapper.Map<IncidentReportDto>(incidentReport);
        }

        public async Task<IEnumerable<IncidentReportDto>> GetAllIncidentReportsAsync(int pageNumber, int pageSize, int? severity = null, string location = null)
        {
            var incidentReports = await _repository.GetAllAsync(pageNumber, pageSize, severity, location);
            return _mapper.Map<IEnumerable<IncidentReportDto>>(incidentReports);
        }

        public async Task<IncidentReportDto> CreateIncidentReportAsync(CreateIncidentReportDto createDto)
        {
            var incidentReport = _mapper.Map<IncidentReport>(createDto);
            incidentReport.Id = Guid.NewGuid();
            incidentReport.Status = "pending";

            await _repository.CreateAsync(incidentReport);
            await _repository.SaveChangesAsync();

            return _mapper.Map<IncidentReportDto>(incidentReport);
        }

        public async Task<IncidentReportDto> UpdateIncidentStatusAsync(Guid id, UpdateIncidentStatusDto updateDto)
        {
            var incidentReport = await _repository.GetByIdAsync(id);
            if (incidentReport == null)
            {
                return null;
            }

            incidentReport.Status = updateDto.Status;
            await _repository.UpdateAsync(incidentReport);
            await _repository.SaveChangesAsync();

            return _mapper.Map<IncidentReportDto>(incidentReport);
        }

        public async Task DeleteIncidentReportAsync(Guid id)
        {
            var incidentReport = await _repository.GetByIdAsync(id);
            if (incidentReport != null)
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveChangesAsync();
            }
        }
    }
}

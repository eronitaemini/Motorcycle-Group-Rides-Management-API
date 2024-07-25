using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentReportsController: ControllerBase
    {

        private readonly IIncidentReportRepository _repository;
        private readonly IMapper _mapper;

        public IncidentReportsController(IIncidentReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("CreateIncidentReport")]
        public async Task<IActionResult> ReportIncident([FromBody] CreateIncidentReportDto createDto)
        {
            var incidentReport = _mapper.Map<IncidentReport>(createDto);
            incidentReport.Id = Guid.NewGuid();
            incidentReport.Status = "pending";
            await _repository.CreateAsync(incidentReport);
            await _repository.SaveChangesAsync();
            return Ok("Incident report submitted successfully.");
        }

        [HttpGet("GetAllIncidentReports")]
        public async Task<IActionResult> GetAllIncidentReports()
        {
            var incidentReports = await _repository.GetAllAsync();
            var incidentReportsDto = _mapper.Map<List<CreateIncidentReportDto>>(incidentReports);
            return Ok(incidentReportsDto);
        }

        [HttpGet("GetIncidentReportById")]
        public async Task<IActionResult> GetIncidentReportById(Guid id)
        {
            var incidentReport = await _repository.GetByIdAsync(id);
            if (incidentReport == null)
            {
                return NotFound();
            }
            var incidentReportDto = _mapper.Map<IncidentReportDto>(incidentReport);
            return Ok(incidentReportDto);
        }


        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateIncidentStatus(Guid id, [FromBody] UpdateIncidentStatusDto updateDto)
        {
            var incidentReport = await _repository.GetByIdAsync(id);
            if (incidentReport == null)
            {
                return NotFound("Incident report not found");
            }

            _mapper.Map(updateDto, incidentReport);
            await _repository.UpdateAsync(incidentReport);
            await _repository.SaveChangesAsync();

            return Ok("Incident status updated successfully.");
        }

        [HttpDelete("DeleteReport")]
        public async Task<IActionResult> DeleteIncidentReport(Guid id)
        {
            var incidentReport = await _repository.GetByIdAsync(id);
            if (incidentReport == null)
            {
                return NotFound("Incident report not found");
            }

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();

            return Ok("Incident report deleted successfully.");
        }
    

}
}

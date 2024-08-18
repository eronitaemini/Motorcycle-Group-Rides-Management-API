

using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Motorcycle_Group_Rides_Management_API.Controllers;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.IncidentReportProfile; // Ensure this matches your actual namespace

namespace Motorcycle_Group_Rides_Management_API.Tests
{
    public class Testings
    {
        private readonly Mock<IIncidentReportRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly IncidentReportsController _controller;
        private Mock<IIncidentReportRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public Testings()
        {
            _repositoryMock = new Mock<IIncidentReportRepository>();
            _mapperMock = new Mock<IMapper>();
            _controller = new IncidentReportsController(_repositoryMock.Object, _mapperMock.Object);
        }




        [Fact]
        public async Task ReportIncident_ShouldReturnOk_WhenIncidentReportIsCreated()
        {
            // Arrange
            var createDto = new CreateIncidentReportDto();
            var incidentReport = new IncidentReport { Id = Guid.NewGuid(), Status = "pending" };
            _mapperMock.Setup(m => m.Map<IncidentReport>(createDto)).Returns(incidentReport);
            _repositoryMock.Setup(r => r.CreateAsync(incidentReport)).Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.ReportIncident(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Incident report submitted successfully.", okResult.Value);
        }





        [Fact]
        public async Task GetAllIncidentReports_ShouldReturnOk_WithIncidentReportsDto()
        {
            // Arrange
            var incidentReports = new List<IncidentReport> { new IncidentReport() };
            var incidentReportsDto = new List<IncidentReportDto> { new IncidentReportDto() };
            _repositoryMock.Setup(r => r.GetAllAsync(1, 10, null, null)).ReturnsAsync(incidentReports);
            _mapperMock.Setup(m => m.Map<List<IncidentReportDto>>(incidentReports)).Returns(incidentReportsDto);

            // Act
            var result = await _controller.GetAllIncidentReports();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(incidentReportsDto, okResult.Value);
        }

        [Fact]
        public async Task GetIncidentReportById_ShouldReturnNotFound_WhenReportDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((IncidentReport)null);

            // Act
            var result = await _controller.GetIncidentReportById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateIncidentStatus_ShouldReturnOk_WhenIncidentReportIsUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateIncidentStatusDto();
            var incidentReport = new IncidentReport { Id = id };
            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(incidentReport);
            _mapperMock.Setup(m => m.Map(updateDto, incidentReport));
            _repositoryMock.Setup(r => r.UpdateAsync(incidentReport)).Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.UpdateIncidentStatus(id, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Incident status updated successfully.", okResult.Value);
        }




        [Fact]
        public async Task DeleteIncidentReport_ShouldReturnOk_WhenReportIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            var incidentReport = new IncidentReport { Id = id };
            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(incidentReport);
            _repositoryMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.DeleteIncidentReport(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Incident report deleted successfully.", okResult.Value);
        }



    }
}

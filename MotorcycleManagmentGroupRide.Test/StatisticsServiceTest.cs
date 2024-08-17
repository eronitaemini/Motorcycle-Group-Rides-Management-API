using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleManagmentGroupRide.Test
{
    public class StatisticsServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IStatisticsRepository> _mockRepository;
        private readonly Mock<ILogger<StatisticsService>> _mockLogger;
        private readonly StatisticsService _service;

        public StatisticsServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IStatisticsRepository>();
            _mockLogger = new Mock<ILogger<StatisticsService>>();
            _service = new StatisticsService(_mockMapper.Object, _mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetIncidentCountByLocationAsync_ReturnsCorrectIncidentLocationCountDtos()
        {
            // Arrange
            var incidentCounts = new Dictionary<string, int>
        {
            { "Location1", 10 },
            { "Location2", 5 }
        };

            _mockRepository.Setup(repo => repo.GetIncidentCountByLocationAsync())
                .ReturnsAsync(incidentCounts);

            // Act
            var result = await _service.GetIncidentCountByLocationAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Location == "Location1" && r.IncidentCount == 10);
            Assert.Contains(result, r => r.Location == "Location2" && r.IncidentCount == 5);
        }

      
          

        [Fact]
        public async Task GetAverageParticipantsPerRideAsync_ReturnsCorrectAverage()
        {
            // Arrange
            var averageParticipants = 7.5;
            _mockRepository.Setup(repo => repo.GetAverageParticipantsPerRideAsync())
                .ReturnsAsync(averageParticipants);

            // Act
            var result = await _service.GetAverageParticipantsPerRideAsync();

            // Assert
            Assert.Equal(averageParticipants, result);
        }

        [Fact]
        public async Task GetAverageFeedbackRatingPerRideAsync_ReturnsCorrectRatings()
        {
            // Arrange
            var feedbackRatings = new List<KeyValuePair<Guid, double>>
        {
            new KeyValuePair<Guid, double>(Guid.NewGuid(), 4.5),
            new KeyValuePair<Guid, double>(Guid.NewGuid(), 3.8)
        };

            _mockRepository.Setup(repo => repo.GetAverageFeedbackRatingPerRideAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(feedbackRatings);

            // Act
            var result = await _service.GetAverageFeedbackRatingPerRideAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Value == 4.5);
            Assert.Contains(result, r => r.Value == 3.8);
        }

        [Fact]
        public async Task GetAverageFeedbackRatingPerRideAsync_LogsCorrectInformation()
        {
            // Arrange
            var feedbackRatings = new List<KeyValuePair<Guid, double>>
        {
            new KeyValuePair<Guid, double>(Guid.NewGuid(), 4.5)
        };

            _mockRepository.Setup(repo => repo.GetAverageFeedbackRatingPerRideAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(feedbackRatings);

            // Act
            var result = await _service.GetAverageFeedbackRatingPerRideAsync("search", 1, 5, 1, 10);

            // Assert
            _mockLogger.Verify(
                log => log.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Fetching average feedback ratings with searchQuery: search, minRating: 1, maxRating: 5, pageNumber: 1, pageSize: 10")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            _mockLogger.Verify(
                log => log.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("1 average feedback ratings fetched")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }

}

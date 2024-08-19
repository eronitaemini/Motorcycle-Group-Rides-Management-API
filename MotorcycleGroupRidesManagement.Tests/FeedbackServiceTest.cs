using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Services;
using Xunit;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;


namespace MotorcycleGroupRidesManagement.Tests
{
    public class FeedbackServiceTest
    {
        private readonly Mock<IFeedbackRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<FeedbackService>> _mockLogger;
        private readonly FeedbackService _service;

        public FeedbackServiceTest()
        {
            _mockRepo = new Mock<IFeedbackRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<FeedbackService>>();
            _service = new FeedbackService(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetFeedbacksAsync_ReturnsFeedbacks()
        {
            var feedbacks = new List<Feedback>
        {
            new Feedback { FeedbackId = Guid.NewGuid(), Comments = "Great ride!", Rating = 5 }
        };

            _mockRepo.Setup(repo => repo.GetFeedbacksAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>()))
                     .ReturnsAsync(feedbacks);

            var result = await _service.GetFeedbacksAsync("Great", "Rating", true, 1, 10);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Great ride!", result.First().Comments);
        }

        [Fact]
        public async Task CreateAsync_ValidFeedback_CreatesFeedback()
        {
            var createFeedbackDto = new CreateFeedbackDto { Comments = "Excellent ride!", Rating = 5 };
            var feedback = new Feedback { Comments = "Excellent ride!", Rating = 5 };

            _mockMapper.Setup(m => m.Map<Feedback>(createFeedbackDto)).Returns(feedback);

            await _service.CreateFeedbackAsync(createFeedbackDto);

            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Feedback>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

   /*     [Fact]
        public async Task UpdateAsync_ValidFeedback_UpdatesFeedback()
        {
            var feedback = new Feedback { FeedbackId = Guid.NewGuid(), Comments = "Updated feedback", Rating = 4 };
            var updateFeedbackDto = GetUpdateFeedbackDto(feedback);

            _mockMapper.Setup(m => m.Map<Feedback>(updateFeedbackDto)).Returns(feedback);

            await _service.UpdateFeedbackAsync(updateFeedbackDto);

            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Feedback>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        public static UpdateFeedbackDto GetUpdateFeedbackDto(Feedback feedback)
        {
            return new UpdateFeedbackDto { FeedbackId = feedback.FeedbackId, Comments = "Updated feedback", Rating = 4 };
        }
        */
        [Fact]
        public async Task DeleteAsync_ValidId_DeletesFeedback()
        {
            var feedbackId = Guid.NewGuid();

            await _service.DeleteFeedbackAsync(feedbackId);

            _mockRepo.Verify(repo => repo.DeleteAsync(feedbackId), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetFeedbackByIdAsync_ReturnsFeedback()
        {
            var feedbackId = Guid.NewGuid();
            var feedback = new Feedback { FeedbackId = feedbackId, Comments = "Feedback comment", Rating = 3 };

            _mockRepo.Setup(repo => repo.GetByIdAsync(feedbackId)).ReturnsAsync(feedback);

            var result = await _service.GetFeedbackByIdAsync(feedbackId);

            Assert.NotNull(result);
            Assert.Equal(feedbackId, result.FeedbackId);
            Assert.Equal("Feedback comment", result.Comments);
        }
    }

using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Showtime.Commands;
using BookingSystem.Domain.Models.Showtime.Exceptions;
using BookingSystem.Domain.Services.ShowTimeManagement.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using BookingSystem.Domain.Models.Showtime;

namespace UnitTests.BookingSystem.Domain.Services.ShowTimeManagement.CommandHandlers
{
    public class DeleteShowTimeCommandHandlerTests
    {
        private readonly Mock<IShowTimeRepository> _mockShowTimeRepository;
        private readonly Mock<ILogger<DeleteShowTimeCommandHandler>> _mockLogger;
        private readonly DeleteShowTimeCommandHandler _handler;

        public DeleteShowTimeCommandHandlerTests()
        {
            _mockShowTimeRepository = new Mock<IShowTimeRepository>();
            _mockLogger = new Mock<ILogger<DeleteShowTimeCommandHandler>>();
            _handler = new DeleteShowTimeCommandHandler(_mockShowTimeRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_WithNonExistentShowTime_ThrowsShowTimeCreationException()
        {
            // Arrange
            var command = new DeleteShowTimeCommand { Id = 500 };
            _mockShowTimeRepository.Setup(repo => repo.DeleteAsync(command.Id)).ReturnsAsync((ShowTime)null);

            // Act & Assert
            await Assert.ThrowsAsync<ShowTimeCreationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithExistingShowTime_DeletesShowTimeSuccessfully()
        {
            // Arrange
            var existingShowTime = new ShowTime { Id = 1 }; 
            var command = new DeleteShowTimeCommand { Id = existingShowTime.Id };
            _mockShowTimeRepository.Setup(repo => repo.DeleteAsync(command.Id)).ReturnsAsync(existingShowTime);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingShowTime.Id, result.Id);
        }
    }
}

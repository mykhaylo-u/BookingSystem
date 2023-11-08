using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Showtime.Commands;
using BookingSystem.Domain.Models.Showtime.Exceptions;
using BookingSystem.Domain.Services.ShowtimeManagement.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Domain.Models.Showtime;

namespace UnitTests.BookingSystem.Domain.Services.ShowTimeManagement.CommandHandlers
{
    public class UpdateShowTimeCommandHandlerTests
    {
        private readonly Mock<IShowTimeRepository> _mockShowTimeRepository;
        private readonly Mock<ILogger<UpdateShowTimeCommandHandler>> _mockLogger;
        private readonly UpdateShowTimeCommandHandler _handler;

        public UpdateShowTimeCommandHandlerTests()
        {
            _mockShowTimeRepository = new Mock<IShowTimeRepository>();
            _mockLogger = new Mock<ILogger<UpdateShowTimeCommandHandler>>();
            _handler = new UpdateShowTimeCommandHandler(_mockShowTimeRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_WithPastStartDateTime_ThrowsShowTimeCreationException()
        {
            // Arrange
            var command = new UpdateShowTimeCommand
            {
                StartDateTime = DateTime.Now.AddDays(-1),
            };

            // Act & Assert
            await Assert.ThrowsAsync<ShowTimeCreationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNonExistentShowTime_ThrowsShowTimeCreationException()
        {
            // Arrange
            var command = new UpdateShowTimeCommand
            {
                Id = 1,
                StartDateTime = DateTime.Now.AddDays(1),
            };

            _mockShowTimeRepository.Setup(repo => repo.UpdateAsync(command.Id, It.IsAny<ShowTime>())).ReturnsAsync((ShowTime)null);

            // Act & Assert
            await Assert.ThrowsAsync<ShowTimeCreationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithValidData_UpdatesShowTimeSuccessfully()
        {
            // Arrange
            var command = new UpdateShowTimeCommand
            {
                Id = 1,
                StartDateTime = DateTime.Now.AddDays(1),
                EndDateTime = DateTime.Now.AddDays(1).AddHours(2),
                TicketPrice = 100,
                Seats = new List<Seat>()
            };

            var existingShowTime = new ShowTime
            {
            };

            _mockShowTimeRepository.Setup(repo => repo.UpdateAsync(command.Id, It.IsAny<ShowTime>())).ReturnsAsync(existingShowTime);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingShowTime, result);
            _mockShowTimeRepository.Verify(repo => repo.UpdateAsync(command.Id, It.IsAny<ShowTime>()), Times.Once);
        }
    }
}
